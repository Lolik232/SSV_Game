using System;
using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] private SettingsSO _defaultSettings = default;

        [Header("Broadcasting")]
        [SerializeField] private FloatEventChannelSO _masterVolumeEventChannelSO = default;
        [SerializeField] private FloatEventChannelSO _musicVolumeEventChannelSO   = default;
        [SerializeField] private FloatEventChannelSO _effectsVolumeEventChannelSO = default;


        private void Awake()
        {
            if (_saveSystem.LoadSettingsFromDisk() == false)
            {
                _saveSystem.SavedSettings.SaveSettings(_defaultSettings);
            }
            _currentSettings.LoadSavedSettings(_saveSystem.SavedSettings);
        }

        private void OnEnable()
        {
            _saveSettingsEventChannelSO.OnEventRaised += SaveSettings;
        }

        private void Start()
        {
            StartCoroutine(SetSettings());
        }

        private void OnDisable()
        {
            _saveSettingsEventChannelSO.OnEventRaised -= SaveSettings;
        }

        private IEnumerator SetSettings()
        {
            _masterVolumeEventChannelSO.RaiseEvent(_currentSettings.masterVolume);
            _musicVolumeEventChannelSO.RaiseEvent(_currentSettings.musicVolume);
            _effectsVolumeEventChannelSO.RaiseEvent(_currentSettings.effectsVolume);
            yield return null;
        }

        private void SaveSettings()
        {
            StartCoroutine(SaveSettingsCoroutine());
        }

        private IEnumerator SaveSettingsCoroutine()
        {
            _saveSystem.SaveSettingsOnDisk();
            yield return null;
        }
    }
}