using System;
using System.Collections;
using System.Collections.Generic;
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
    public UnityEvent onAlternativeEvent;
    
    private void OnEnable()
    {
        Action confirmCallback = null;
        Action declineCallback = null;
        Action alternativeCallback = null;

        if (onConfirmEvent.GetPersistentEventCount() > 0)
        {
            confirmCallback = onConfirmEvent.Invoke;
        }
        if (onDeclineEvent.GetPersistentEventCount() > 0)
        {
            declineCallback = onDeclineEvent.Invoke;
        }
        if (onAlternativeEvent.GetPersistentEventCount() > 0)
        {
            alternativeCallback = onAlternativeEvent.Invoke;
        }
        
        ModalWindowController.instance.ModalWindow.ShowWindow(title, content, confirmCallback, declineAction: declineCallback, alternativeAction: alternativeCallback);
    }

    private void OnDisable()
    {
        if (buttonToSelectOnExit.interactable)
        {
            buttonToSelectOnExit.Select();
        }
        else
        {
            buttonToSelectOnExit1.Select();
        }
    }
}
