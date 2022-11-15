using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "UI/Volume/Volume")]
public class UISettingsManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup mixer;

    public void ChangeMaster(float volume)
    {
        mixer.audioMixer.SetFloat("MasterVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeMusic(float volume)
    {
        mixer.audioMixer.SetFloat("MusicVolume", Mathf.Lerp(-80, 0, volume));
    }

    public void ChangeFX(float volume)
    {
        mixer.audioMixer.SetFloat("FXVolume", Mathf.Lerp(-80, 0, volume));

    }

}
