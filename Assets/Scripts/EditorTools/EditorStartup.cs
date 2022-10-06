using System;
using All.Events;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace EditorTools
{
    public class EditorStartup : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private GameSceneSO m_thisScene = default;
        [SerializeField] private GameSceneSO m_managers = default;
        [SerializeField] private AssetReference m_notifyEditorStartupChannel = default;
        [SerializeField] private VoidEventChannelSO m_onReadySceneChannel = default;

        private AsyncOperationHandle<SceneInstance> m_managersSceneLoadingOpHandle = default;

        private bool m_editorStartup = false;

        private void Awake()
        {
            if (SceneManager.GetSceneByName(m_managers.sceneReference.editorAsset.name).isLoaded == false)
            {
                m_editorStartup = true;
            }
        }

        private void Start()
        {
            if (m_editorStartup)
            {
                m_managers.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnLoadManagers;
            }
        }

        private void OnLoadManagers(AsyncOperationHandle<SceneInstance> obj)
        {
            m_notifyEditorStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoad;
        }

        private void OnNotifyChannelLoad(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            if (m_thisScene != null)
            {
                obj.Result.RaiseEvent(m_thisScene);
            }
            else
            {
                m_onReadySceneChannel.RaiseEvent();
            }
        }
#endif
    }
}