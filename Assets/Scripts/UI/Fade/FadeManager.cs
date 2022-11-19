using System;
using System.Collections;
using All.Events;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private FadeChannelSO _fadeEventChannelSO = default;
    [SerializeField] private Image         _fadeComponent;

    private void OnEnable()
    {
        _fadeEventChannelSO.OnEventRaised += StartFade;
    }

    private void StartFade(bool fadeIn, float duration, Color desiredColor)
    {
        // _fadeComponent.color = desiredColor;
        _fadeComponent.CrossFadeAlpha(fadeIn ? 0 : 1, duration, true);
    }

    private void OnDisable()
    {
        _fadeEventChannelSO.OnEventRaised -= StartFade;
    }
}