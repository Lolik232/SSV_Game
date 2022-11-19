using System.Collections;

using All.Events;

using Systems.SaveSystem;

using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI
{
    public class StartGameController : MonoBehaviour
    {
        [SerializeField] private GameSceneSO _sceneToLoad = default;

        [SerializeField] private UIInputSO  _uiInputSo  = default;
        [SerializeField] private SaveSystem _saveSystem = default;

        [SerializeField] private LoadEventChannelSO _loadEventChannelSO = default;

        private void OnEnable()
        {
            _uiInputSo.ContinueGame.AddListener(ContinueGame);
            _uiInputSo.StartNewGame.AddListener(NewGame);
        }


        private void OnDisable()
        {
            _uiInputSo.ContinueGame.RemoveListener(ContinueGame);
            _uiInputSo.StartNewGame.RemoveListener(NewGame);
        }


        private void NewGame()
        {
            _saveSystem.RemoveGameFromDisk();
            _loadEventChannelSO.RaiseEvent(_sceneToLoad, true, false);
        }

        private void ContinueGame()
        {
            StartCoroutine(LoadSaveGame());
        }

        private IEnumerator LoadSaveGame()
        {
            _saveSystem.LoadDataFromDisk();

            var locationGuid = _saveSystem.save.locationID;
            var asyncOp      = Addressables.LoadAssetAsync<LocationSO>(locationGuid);
            yield return asyncOp;

            if (asyncOp.Status == AsyncOperationStatus.Succeeded)
            {
                LocationSO locationSO = asyncOp.Result;
                _loadEventChannelSO.RaiseEvent(locationSO, true, false);
            }
        }
    }
}