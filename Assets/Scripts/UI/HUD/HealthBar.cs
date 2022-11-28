using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthEventChannelSO _healthEventChannelSo;
    private Slider _healthSlider;

    [SerializeField] private Sprite _fullHeart;
    [SerializeField] private Sprite _crackedHeart;
    [SerializeField] private Sprite _brokenHeart;

    [SerializeField] private Image _heartImage;

    private const float CrackedBound = 0.6f;
    private const float BrokenBound = 0.1f;

    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
    }
    
    private void Start()
    {
        _heartImage.sprite = _fullHeart;
    }

    private void OnEnable()
    {
        _healthEventChannelSo.OnEventRaised += SetHealth;
    }

    private void OnDisable()
    {
        _healthEventChannelSo.OnEventRaised -= SetHealth;
    }

    private void SetHealth(float health, float maxHealth)
    {
        _healthSlider.maxValue = maxHealth;
        _healthSlider.value = health;

        var ratio = health / maxHealth;
        if (ratio > CrackedBound)
        {
            _heartImage.sprite = _fullHeart;
        } else if (ratio > BrokenBound)
        {
            _heartImage.sprite = _crackedHeart;
        }
        else
        {
            _heartImage.sprite = _brokenHeart;
        }
    }
}
