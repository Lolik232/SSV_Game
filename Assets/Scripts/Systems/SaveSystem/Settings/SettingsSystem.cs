using System;
using All.Events;
using Systems.SaveSystem.Settings.ScriptableObjects;
using UnityEngine;

namespace Systems.SaveSystem.Settings
{
    public class SettingsSystem : MonoBehaviour
    {
        [SerializeField] private VoidEventChannelSO _saveSettingsEventChannelSO = default;
        [SerializeField] private SaveSystem         _saveSystem                 = default;
        [SerializeField] private SettingsSO         _currentSettings            = default;
        
        [SerializeField] private FloatEventChannelSO _masterVolumeEventChannelSO  = default;
        [SerializeField] private FloatEventChannelSO _musicVolumeEventChannelSO   = default;
        [SerializeField] private FloatEventChannelSO _effectsVolumeEventChannelSO = default;

        private void Awake()
        {
            _saveSystem.LoadSettingsFromDisk();
            SetSettings();
        }

        private void OnEnable()
        {
            _saveSettingsEventChannelSO.OnEventRaised += SaveSettings;
        }

        private void OnDisable()
        {
            _saveSettingsEventChannelSO.OnEventRaised -= SaveSettings;
        }

        private void SetSettings()
        {
            _masterVolumeEventChannelSO.RaiseEvent(_currentSettings.masterVolume);
            _musicVolumeEventChannelSO.RaiseEvent(_currentSettings.musicVolume);
            _effectsVolumeEventChannelSO.RaiseEvent(_currentSettings.effectsVolume);
        }

        private void SaveSettings()
        {
            _saveSystem.SaveSettingsOnDisk();
        }
    }
}