using System;
using System.Collections;
using All.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameSceneSO m_scene;

        /// <summary>
        /// Listening load location chan to get location to load
        /// </summary>
        [FormerlySerializedAs("m_LoadLocationChan")]
        [Header("Listening")]
        [SerializeField] private LoadEventChannelSO m_loadLocationChan = default;
#if UNITY_EDITOR
        [SerializeField] private LoadEventChannelSO m_loadFromEditorChan = default;
#endif
        [Header("Broadcasting")]
        [SerializeField] private BoolEventChannelSO m_toggleLoadingScreenChan = default;
        [SerializeField]
        private VoidEventChannelSO m_onSceneReadyChan = default; // to scene managers. Spawn player, etc.
        [SerializeField] private FadeChannelSO m_fadeRequestChan = default;

        private AsyncOperationHandle<SceneInstance> m_loadingOperationHandle;
        private AsyncOperationHandle<SceneInstance> m_gameplayManagerLoadingOpHandle;

        private GameSceneSO m_sceneToLoad;
        private GameSceneSO m_currentlyLoadedScene;
        private bool m_showLoadingScreen = false;

        private SceneInstance m_gameplayManagerSceneInstance = default;
        private float m_fadeDuration = .5f;
        private bool m_isLoading = false;


#if UNITY_EDITOR
        private void OnEditorLoad(GameSceneSO scene, bool showLoadingScreen, bool fadeScreen)
        {
            m_currentlyLoadedScene = scene;
            if (m_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Location)
            {
                // //Gameplay managers is loaded synchronously
                // _gameplayManagerLoadingOpHandle = _gameplayScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
                // _gameplayManagerLoadingOpHandle.WaitForCompletion();
                // _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

                StartGameplay();
            }
        }
#endif

        private void Awake()
        {
            m_currentlyLoadedScene = m_scene;
        }

        private void OnEnable()
        {
            m_loadLocationChan.OnEventRaised += LoadLocation;
#if UNITY_EDITOR
            m_loadFromEditorChan.OnEventRaised += OnEditorLoad;
#endif
        }

        private void OnDisable()
        {
            m_loadLocationChan.OnEventRaised -= LoadLocation;
#if UNITY_EDITOR
            m_loadFromEditorChan.OnEventRaised -= OnEditorLoad;
#endif
        }

        
        private void LoadLocation(GameSceneSO scene, bool showLoadingScreen, bool fadeScreen)
        {
            if (m_isLoading) { return; } // if scene just loading

            m_sceneToLoad = scene;
            m_showLoadingScreen = showLoadingScreen;
            m_isLoading = true;


            // if (m_gameplayManagerSceneInstance.Scene == null ||
            //     m_gameplayManagerSceneInstance.Scene.isLoaded == false)
            // {
            //     m_gameplayManagerLoadingOpHandle = m_scene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
            //     m_gameplayManagerLoadingOpHandle.Completed += OnGameplayManagersLoaded;
            // } else { StartCoroutine(UnloadPreviousScene()); }

            StartCoroutine(UnloadPreviousScene());
        }

        private void OnGameplayManagersLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            m_gameplayManagerSceneInstance = m_gameplayManagerLoadingOpHandle.Result;

            StartCoroutine(UnloadPreviousScene());
        }

        private IEnumerator UnloadPreviousScene()
        {
            // TODO: disable all inputs
            m_fadeRequestChan.RaiseEvent(m_fadeDuration);

            yield return new WaitForSeconds(m_fadeDuration);

            if (m_currentlyLoadedScene != null)
            {
                if (m_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
                {
                    m_currentlyLoadedScene.sceneReference.UnLoadScene();
                }
#if UNITY_EDITOR
                else { SceneManager.UnloadSceneAsync(m_currentlyLoadedScene.sceneReference.editorAsset.name); }
#endif
            }


            LoadNewScene();
        }

        private void LoadNewScene()
        {
            if (m_showLoadingScreen) { m_toggleLoadingScreenChan.RaiseEvent(true); }

            m_loadingOperationHandle = m_sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);

            m_loadingOperationHandle.Completed += OnNewSceneLoaded;
        }

        private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            m_currentlyLoadedScene = m_sceneToLoad;

            Scene s = obj.Result.Scene;
            SceneManager.SetActiveScene(s);
            LightProbes.TetrahedralizeAsync();

            m_isLoading = false;
            if (m_showLoadingScreen) { m_toggleLoadingScreenChan.RaiseEvent(false); }

            m_fadeRequestChan.RaiseEvent(m_fadeDuration);

            StartGameplay();
        }

        private void StartGameplay()
        {
            m_onSceneReadyChan.RaiseEvent();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}