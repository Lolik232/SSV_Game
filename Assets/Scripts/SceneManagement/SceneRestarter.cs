using System;
using All.Events;
using Systems.SaveSystem;
using UnityEngine;

namespace SceneManagement
{
    public class SceneRestarter : MonoBehaviour
    {
        [SerializeField]private LocationSO _currentLocation = default;

        [Header("Listening to")]
        [SerializeField] private LoadEventChannelSO _locationLoadEventChannelSO = default;
        [SerializeField] private VoidEventChannelSO _onDeadEventChannelSO = default;


        [SerializeField] private SaveSystem _saveSystem = default;

        private void OnEnable()
        {
            _onDeadEventChannelSO.OnEventRaised       += Restart;
            _locationLoadEventChannelSO.OnEventRaised += UpdateCurrentLocation;
        }

        private void OnDisable()
        {
            _onDeadEventChannelSO.OnEventRaised       -= Restart;
            _locationLoadEventChannelSO.OnEventRaised -= UpdateCurrentLocation;
        }


        private void UpdateCurrentLocation(GameSceneSO location, bool loadingScreen, bool fadeIn)
        {
            var locationSO = location as LocationSO;

            _currentLocation = locationSO;
        }

        private void Restart()
        {
            if (_currentLocation == null) return;

            // _saveSystem.save.locationID
            // _saveSystem.LoadDataFromDisk();

            _locationLoadEventChannelSO.RaiseEvent(_currentLocation, true, true);
        }
    }
}