using System;
using All.Events;
using Input;
using Systems.SaveSystem.Settings;
using Systems.SaveSystem.Settings.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class UISettingsManager : MonoBehaviour
{
    [SerializeField] private FloatEventChannelSO _masterVolumeEventChannelSO  = default;
    [SerializeField] private FloatEventChannelSO _musicVolumeEventChannelSO   = default;
    [SerializeField] private FloatEventChannelSO _effectsVolumeEventChannelSO = default;
    [SerializeField] private VoidEventChannelSO  _saveSettingsEventChannelSO  = default;

    [SerializeField] private SettingsSO _settings;
    [SerializeField] private SettingsSO _defaultSettings;

    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;

    private readonly SettingsSave _currentSettings = new SettingsSave();
    
    public void Setup()
    {
        Setup(_settings);
    }
    
    public void Setup(SettingsSO settingsSo)
    {
        _masterVolumeSlider.SetValueWithoutNotify(settingsSo.masterVolume);
        _musicVolumeSlider.SetValueWithoutNotify(settingsSo.musicVolume);
        _effectsVolumeSlider.SetValueWithoutNotify(settingsSo.effectsVolume);
        
        _currentSettings.SaveSettings(settingsSo);
        
        ChangeMaster(settingsSo.masterVolume);
        ChangeMusic(settingsSo.musicVolume);
        ChangeFX(settingsSo.effectsVolume);
    }

    public void ChangeMaster(float volume)
    {
        _currentSettings.masterVolume = volume;
        _masterVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void ChangeMusic(float volume)
    {
        _currentSettings.musicVolume = volume;
        _musicVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void ChangeFX(float volume)
    {
        _currentSettings.effectsVolume = volume;
        _effectsVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void SaveSettings()
    {
        _settings.LoadSavedSettings(_currentSettings);
        _saveSettingsEventChannelSO.RaiseEvent();
    }

    public void ResetToDefault()
    {
        _currentSettings.SaveSettings(_defaultSettings);
        Setup(_defaultSettings);
    }
    
    
    public void Reset()
    {
        Setup();
    }
}