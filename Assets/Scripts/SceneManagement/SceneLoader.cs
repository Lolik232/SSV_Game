using System.Collections;

using All.Events;

using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [FormerlySerializedAs("m_scene")]
        [SerializeField] private GameSceneSO _scene;

        /// <summary>
        /// Listening load location chan to get location to load
        /// </summary>
        [Header("Listening")]
        [FormerlySerializedAs("m_LoadLocationChan")]
        [SerializeField] private LoadEventChannelSO _loadLocationChan = default;
#if UNITY_EDITOR
        [FormerlySerializedAs("m_loadFromEditorChan")]
        [SerializeField] private LoadEventChannelSO _loadFromEditorChan = default;
#endif
        [Header("Broadcasting")]
        [FormerlySerializedAs("m_toggleLoadingScreenChan")]
        [SerializeField] private BoolEventChannelSO _toggleLoadingScreenChan = default;
        [FormerlySerializedAs("m_onSceneReadyChan")]
        [SerializeField]
        private VoidEventChannelSO _onSceneReadyChan = default; // to scene managers. Spawn entity, etc.
        [FormerlySerializedAs("m_fadeRequestChan")]
        [SerializeField] private FadeChannelSO _fadeRequestChan = default;

        private AsyncOperationHandle<SceneInstance> _loadingOperationHandle;
        private AsyncOperationHandle<SceneInstance> _gameplayManagerLoadingOpHandle;

        private GameSceneSO _sceneToLoad;
        private GameSceneSO _currentlyLoadedScene;
        private bool        _showLoadingScreen = false;

        private SceneInstance _gameplayManagerSceneInstance = default;
        private float         _fadeDuration                 = .5f;
        private bool          _isLoading                    = false;


#if UNITY_EDITOR
        private void OnEditorLoad(GameSceneSO scene, bool showLoadingScreen, bool fadeScreen)
        {
            _currentlyLoadedScene = scene;
            if (_currentlyLoadedScene.sceneType == GameSceneSO.GameSceneType.Location)
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
            _currentlyLoadedScene = _scene;
        }

        private void OnEnable()
        {
            _loadLocationChan.OnEventRaised += LoadLocation;
#if UNITY_EDITOR
            _loadFromEditorChan.OnEventRaised += OnEditorLoad;
#endif
        }

        private void OnDisable()
        {
            _loadLocationChan.OnEventRaised -= LoadLocation;
#if UNITY_EDITOR
            _loadFromEditorChan.OnEventRaised -= OnEditorLoad;
#endif
        }


        private void LoadLocation(GameSceneSO scene, bool showLoadingScreen, bool fadeScreen)
        {
            if (_isLoading)
            {
                return;
            } // if scene just loading

            _sceneToLoad = scene;
            _showLoadingScreen = showLoadingScreen;
            _isLoading = true;


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
            _gameplayManagerSceneInstance = _gameplayManagerLoadingOpHandle.Result;

            StartCoroutine(UnloadPreviousScene());
        }

        private IEnumerator UnloadPreviousScene()
        {
            // TODO: disable all inputs
            _fadeRequestChan.RaiseEvent(_fadeDuration);

            yield return new WaitForSeconds(_fadeDuration);

            if (_currentlyLoadedScene != null)
            {
                if (_currentlyLoadedScene.sceneReference.OperationHandle.IsValid())
                {
                    _currentlyLoadedScene.sceneReference.UnLoadScene();
                }
#if UNITY_EDITOR
                else
                {
                    SceneManager.UnloadSceneAsync(_currentlyLoadedScene.sceneReference.editorAsset.name);
                }
#endif
            }


            LoadNewScene();
        }

        private void LoadNewScene()
        {
            if (_showLoadingScreen)
            {
                _toggleLoadingScreenChan.RaiseEvent(true);
            }

            _loadingOperationHandle = _sceneToLoad.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true, 0);
            // m_loadingOperationHandle.
            // m_loadingOperationHandle.PercentComplete
            _loadingOperationHandle.Completed += OnNewSceneLoaded;
        }

        private void OnNewSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
        {
            _currentlyLoadedScene = _sceneToLoad;

            Scene s = obj.Result.Scene;
            SceneManager.SetActiveScene(s);
            LightProbes.TetrahedralizeAsync();

            _isLoading = false;
            if (_showLoadingScreen)
            {
                _toggleLoadingScreenChan.RaiseEvent(false);
            }

            _fadeRequestChan.RaiseEvent(_fadeDuration);

            StartGameplay();
        }

        private void StartGameplay()
        {
            _onSceneReadyChan.RaiseEvent();
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}