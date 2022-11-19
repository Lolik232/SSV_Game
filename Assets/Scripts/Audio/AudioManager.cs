using System;

using All.Events;

using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private string _masterVolumeGroup  = "MasterVolume";
    [SerializeField] private string _musicVolumeGroup   = "MusicVolume";
    [SerializeField] private string _effectsVolumeGroup = "FXVolume";

    [SerializeField] private AudioMixerGroup _audioMixer = default;

    [SerializeField] private FloatEventChannelSO _masterVolumeEventChannelSO  = default;
    [SerializeField] private FloatEventChannelSO _musicVolumeEventChannelSO   = default;
    [SerializeField] private FloatEventChannelSO _effectsVolumeEventChannelSO = default;

    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _effectsVolume = 1f;

    private void OnEnable()
    {
        _masterVolumeEventChannelSO.OnEventRaised += ChangeMaster;
        _musicVolumeEventChannelSO.OnEventRaised += ChangeMusic;
        _effectsVolumeEventChannelSO.OnEventRaised += ChangeFX;
    }

    private void OnDisable()
    {
        _masterVolumeEventChannelSO.OnEventRaised -= ChangeMaster;
        _musicVolumeEventChannelSO.OnEventRaised -= ChangeMusic;
        _effectsVolumeEventChannelSO.OnEventRaised -= ChangeFX;
    }


    public void ChangeMaster(float volume)
    {
        _masterVolume = volume;
        _audioMixer.audioMixer.SetFloat(_masterVolumeGroup, Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeMusic(float volume)
    {
        _musicVolume = volume;
        _audioMixer.audioMixer.SetFloat(_musicVolumeGroup, Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeFX(float volume)
    {
        _effectsVolume = volume;
        _audioMixer.audioMixer.SetFloat(_effectsVolumeGroup, Mathf.Lerp(-80, 0, volume));
    }
}