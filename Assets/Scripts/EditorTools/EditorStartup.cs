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

        [FormerlySerializedAs("m_thisScene")]
        [SerializeField] private GameSceneSO _thisScene = default;
        [FormerlySerializedAs("m_managers")]
        [SerializeField] private GameSceneSO _managers = default;
        [FormerlySerializedAs("m_notifyEditorStartupChannel")]
        [SerializeField] private AssetReference _notifyEditorStartupChannel = default;
        [FormerlySerializedAs("m_onReadySceneChannel")]
        [SerializeField] private VoidEventChannelSO _onReadySceneChannel = default;

        private AsyncOperationHandle<SceneInstance> _managersSceneLoadingOpHandle = default;

        private bool m_editorStartup = false;

        private void Awake()
        {
            if (SceneManager.GetSceneByName(_managers.sceneReference.editorAsset.name).isLoaded == false)
            {
                m_editorStartup = true;
            }
        }

        private void Start()
        {
            if (m_editorStartup)
            {
                _managers.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnLoadManagers;
            }
        }

        private void OnLoadManagers(AsyncOperationHandle<SceneInstance> obj)
        {
            _notifyEditorStartupChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += OnNotifyChannelLoad;
        }

        private void OnNotifyChannelLoad(AsyncOperationHandle<LoadEventChannelSO> obj)
        {
            if (_thisScene != null)
            {
                obj.Result.RaiseEvent(_thisScene);
            }
            else
            {
                _onReadySceneChannel.RaiseEvent();
            }
        }
#endif
    }
}