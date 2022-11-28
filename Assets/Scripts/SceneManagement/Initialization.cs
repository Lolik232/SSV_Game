using All.Events;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private GameSceneSO _managerScene = default;
    [SerializeField] private GameSceneSO _menuToLoad   = default;


    [SerializeField] private LoadEventChannelSO _menuLoadEventChannel = default;
    [SerializeField] private AssetReference _menuLoadChannel = default;
    // [SerializeField] private SaveSystem         _saveSystem           = default;

    private void Start()
    {
        _managerScene.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true).Completed += OnManagersSceneLoaded;
    }

    private void OnManagersSceneLoaded(AsyncOperationHandle<SceneInstance> obj)
    {
        // _menuLoadEventChannel.RaiseEvent(_menuToLoad, true);
        // SceneManager.UnloadSceneAsync(0);
        _menuLoadChannel.LoadAssetAsync<LoadEventChannelSO>().Completed += LoadMainMenu;
    }

    private void LoadMainMenu(AsyncOperationHandle<LoadEventChannelSO> obj)
    {
        obj.Result.RaiseEvent(_menuToLoad, true);
        SceneManager.UnloadSceneAsync(0);
    }
}