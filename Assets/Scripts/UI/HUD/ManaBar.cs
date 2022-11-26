using System;
using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private ManaEventChannelSO _manaEventChannelSo;
    private Slider _manaSlider;

    [SerializeField] private Sprite _fullBottle;
    [SerializeField] private Sprite _crackedBottle;
    [SerializeField] private Sprite _emptyBottle;

    [SerializeField] private Image _bottleImage;

    private const float CrackedBound = 0.6f;
    private const float BrokenBound = 0.1f;

    private void Awake()
    {
        _manaSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _bottleImage.sprite = _fullBottle;
    }

    private void OnEnable()
    {
        _manaEventChannelSo.OnEventRaised += SetMana;
    }

    private void OnDisable()
    {
        _manaEventChannelSo.OnEventRaised -= SetMana;
    }

    private void SetMana(float mana, float maxMana)
    {
        _manaSlider.maxValue = maxMana;
        _manaSlider.value = mana;

        var ratio = mana / maxMana;
        if (ratio > CrackedBound)
        {
            _bottleImage.sprite = _fullBottle;
        }
        else if (ratio > BrokenBound)
        {
            _bottleImage.sprite = _crackedBottle;
        }
        else
        {
            _bottleImage.sprite = _emptyBottle;
        }
    }
}
