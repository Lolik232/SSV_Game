using System;
using All.Events;
using Input;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

[CreateAssetMenu(menuName = "UI/Input/UIInput")]
public class UIInputSO : ScriptableObject, GameInput.IUIActions
{
    public UnityEvent StartNewGame;
    public UnityEvent ContinueGame;

    [NonSerialized] public bool enterPressed;
    [NonSerialized] public bool escPressed;
    [NonSerialized] public bool optionsPressed;
    [NonSerialized] public bool gameOnPause = false;

    [SerializeField] private GameSceneSO locationToLoad;
    // [SerializeField] private LoadEventChannelSO loadLocationChannel;
    private GameInput _gameInput;

    private void OnEnable()
    {
        if (_gameInput == null)
        {
            _gameInput = GameInputSingeltone.GameInput;
        }

        _gameInput.UI.SetCallbacks(this);
    }

    public void OnContinueButtonPressed()
    {
        ContinueGame?.Invoke();
    }

    public void OnNewButtonPressed()
    {
        StartNewGame?.Invoke();
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

    public void OnEnter(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            enterPressed = true;
        } else if (context.canceled)
        {
            enterPressed = false;
        }
    }

    public void OnEscape(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            escPressed  = true;
            gameOnPause = !gameOnPause;
        } else if (context.canceled)
        {
            escPressed = false;
        }
    }
}