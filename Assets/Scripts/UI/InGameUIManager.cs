using System;
using System.Collections;
using System.Collections.Generic;
using All.Events;
using Input;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    // update ui components if ne scene
    [SerializeField] private VoidEventChannelSO _onSceneReadyChannel = default;
    
    [SerializeField] private InGameMenu _inGameMenu;
    [SerializeField] private GameObject _optionsPanel;

    [SerializeField] private GameSceneSO        _mainMenu             = default;
    [SerializeField] private LoadEventChannelSO _menuLoadEventChannel = default;


    [SerializeField] private UIInputSO          _UIInputReader     = default;
    [SerializeField] private VoidEventChannelSO _pauseEventChannel = default;

    private void OnEnable()
    {
        _onSceneReadyChannel.OnEventRaised += ResetUi;
        _pauseEventChannel.OnEventRaised   += OnPause;
    }

    private void OnDisable()
    {
        ResetUi();
        _onSceneReadyChannel.OnEventRaised -= ResetUi;
        _pauseEventChannel.OnEventRaised   -= OnPause;
    }

    private void OnPause()
    {
        _UIInputReader.gameOnPause = true;
        
        Time.timeScale = 0;

        _inGameMenu.ResumeClicked         += OnUnpause;
        _inGameMenu.Closed                += OnUnpause;
        _inGameMenu.QuitGameClicked       += OnQuit;
        _inGameMenu.BackToMainMenuClicked += OnBackToMainMenu;


        // _inGameMenu.gameObject.SetActive(true);


        GameInputSingeltone.GameInput.EnableMenuInput();
    }

    private void OnUnpause()
    {
        
        _UIInputReader.gameOnPause = false;
        
        _inGameMenu.ResumeClicked         -= OnUnpause;
        _inGameMenu.Closed                -= OnUnpause;
        _inGameMenu.QuitGameClicked       -= OnQuit;
        _inGameMenu.BackToMainMenuClicked -= OnBackToMainMenu;

        // _inGameMenu.gameObject.SetActive(false);

        Time.timeScale             = 1;

        GameInputSingeltone.GameInput.EnableGameplayInput();
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnBackToMainMenu()
    {
        _menuLoadEventChannel.RaiseEvent(_mainMenu, false, true);
    }


    private void ResetUi()
    {
        _inGameMenu.gameObject.SetActive(false);
        _optionsPanel.SetActive(false);

        Time.timeScale = 1;
    }
}