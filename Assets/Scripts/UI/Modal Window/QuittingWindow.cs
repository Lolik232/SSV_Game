using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuittingWindow : MonoBehaviour
{
    [SerializeField] private Button buttonToSelectOnExit;
    [SerializeField] private Button buttonToSelectOnExit1;

    [SerializeField] private string title;
    [SerializeField] private string content;

    public UnityEvent onConfirmEvent;
    
    public UnityEvent onDeclineEvent;
    [SerializeField] private bool hasDeclineEvent;
    
    public UnityEvent onAlternativeEvent;
    [SerializeField] private bool hasAlternativeEvent;

    private void OnEnable()
    {
        GameInputSingleton.GameInput.UI.Disable();
        
        Action confirmCallback = onConfirmEvent.Invoke;
        Action declineCallback = null;
        Action alternativeCallback = null;

        if (hasDeclineEvent)
        {
            declineCallback = onDeclineEvent.Invoke;
        }

        if (hasAlternativeEvent)
        {
            alternativeCallback = onAlternativeEvent.Invoke;
        }

        ModalWindowController.instance.ModalWindow.ShowWindow(title, content, confirmCallback,
            declineAction: declineCallback, alternativeAction: alternativeCallback);
    }

    private void OnDisable()
    {
        GameInputSingleton.GameInput.UI.Enable();
        
        if (buttonToSelectOnExit != null && buttonToSelectOnExit.interactable)
        {
            buttonToSelectOnExit.Select();
        }
        else if (buttonToSelectOnExit1 != null)
        {
            buttonToSelectOnExit1.Select();
        }
    }
}