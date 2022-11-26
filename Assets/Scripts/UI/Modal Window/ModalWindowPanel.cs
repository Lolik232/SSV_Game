using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ModalWindowPanel : MonoBehaviour
{
    private Transform _panel;
    
    [SerializeField] private Transform _box;
    
    [Header("Header")] 
    [SerializeField] private Transform _header;
    [SerializeField] private Text _headerMessage;
    
    [Header("Content")] 
    [SerializeField] private Transform _content;
    [SerializeField] private Text _contentMessage;
    
    [Header("Footer")] 
    [SerializeField] private Transform _footer;
    [SerializeField] private Button _confirm;
    [SerializeField] private Button _decline;
    [SerializeField] private Button _alternative;

    private Action _onConfirmAction;
    private Action _onDeclineAction;
    private Action _onAlternativeAction;

    public void Confirm()
    {
        _onConfirmAction?.Invoke();
        Close();
    }
    
    public void Decline()
    {
        _onDeclineAction?.Invoke();
        Close();
    }
    
    public void Alternative()
    {
        _onAlternativeAction?.Invoke();
        Close();
    }

    private void Close()
    {
        _panel.GameObject().SetActive(false);
    }

    private void Show()
    {
        _panel.GameObject().SetActive(true);
    }

    private void Awake()
    {
        _panel = GetComponent<Transform>();
    }
}