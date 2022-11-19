using System;
using All.Events;
using Input;
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

    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    
    private float _masterVolume;
    private float _musicVolume;
    private float _effectsVolume;

    private void Start()
    {
        GameInputSingeltone.GameInput.EnableMenuInput();
    }

    public void Setup()
    {
        _masterVolumeSlider.SetValueWithoutNotify(_settings.masterVolume);
        _musicVolumeSlider.SetValueWithoutNotify(_settings.musicVolume);
        _effectsVolumeSlider.SetValueWithoutNotify(_settings.effectsVolume);

        ChangeMaster(_settings.masterVolume);
        ChangeMusic(_settings.musicVolume);
        ChangeFX(_settings.effectsVolume);
    }

    public void ChangeMaster(float volume)
    {
        _masterVolume = volume;
        _masterVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void ChangeMusic(float volume)
    {
        _musicVolume = volume;
        _musicVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void ChangeFX(float volume)
    {
        _effectsVolume = volume;
        _effectsVolumeEventChannelSO.RaiseEvent(volume);
    }

    public void SaveSettings()
    {
        _settings.masterVolume  = _masterVolume;
        _settings.musicVolume   = _musicVolume;
        _settings.effectsVolume = _effectsVolume;

        _saveSettingsEventChannelSO.RaiseEvent();
    }
    
    public void Reset()
    {
        Setup();
    }
}