using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewGameEnteringWindow : MonoBehaviour
{
    [SerializeField] private Button buttonToSelectOnExit;
    [SerializeField] private Button continueButton;

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
        
        if (!continueButton.interactable)
        {
            confirmCallback.Invoke();
            return;
        }

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
        
        if (continueButton != null && continueButton.interactable)
        {
            buttonToSelectOnExit.Select();
        }
        else if (buttonToSelectOnExit != null)
        {
            buttonToSelectOnExit.Select();
        }
    }
}