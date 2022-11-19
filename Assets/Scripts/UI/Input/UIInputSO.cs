using System;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "UI/Input/UIInput")]
public class UIInputSO : ScriptableObject
{
    public UnityEvent StartNewGame;
    public UnityEvent ContinueGame;

    [NonSerialized] public bool enterPressed;
    [NonSerialized] public bool escPressed;
    [NonSerialized] public bool optionsPressed;

    [SerializeField] private GameSceneSO locationToLoad;
    // [SerializeField] private LoadEventChannelSO loadLocationChannel;

    public void OnEnterDown(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            enterPressed = true;
        }
        else if (context.canceled)
        {
            enterPressed = false;
        }
    }

    public void OnContinueButtonPressed()
    {
        ContinueGame?.Invoke();
    }

    public void OnNewButtonPressed()
    {
        StartNewGame?.Invoke();
    }

    public void OnEscDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            escPressed = true;
        }
        else if (context.canceled)
        {
            escPressed = false;
        }
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    public void OnOptionsButton()
    {
        optionsPressed = true;
    }

    public void OnOptionsBackButton()
    {
        escPressed = true;
    }
}