using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTriggers : MonoBehaviour
{
    [SerializeField] private AudioSource fxSource;
    [SerializeField] private AudioClip uiElementClickSound;
    [SerializeField] private AudioClip uiElementSelectSound;
    [SerializeField] private AudioClip uiSliderSound;
    
    public void ClickSoundPlay()
    {
        fxSource.PlayOneShot(uiElementClickSound);
    }

    public void SelectSoundPlay()
    {
        fxSource.PlayOneShot(uiElementSelectSound);
    }

    public void SliderSoundPlay()
    {
        fxSource.PlayOneShot(uiSliderSound);
    }
}
