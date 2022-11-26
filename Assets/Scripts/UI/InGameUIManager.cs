using System;
using System.Collections;
using System.Collections.Generic;
using All.Events;
using Input;
using Unity.VisualScripting;
using UnityEngine;

public class InGameUIManager : MonoBehaviour
{
    // update ui components if ne scene
    [SerializeField] private VoidEventChannelSO _onSceneReadyChannel = default;

    [SerializeField] private InGameMenu _inGameMenu;
    [SerializeField] private GameObject _optionsPanel;

    [SerializeField] private GameObject _hud;

    [SerializeField] private GameObject _toMainMenuGO;
    [SerializeField] private GameObject _toQuitGO;
    
    private QuittingWindow _quittingToMainMenu;
    private QuittingWindow _quittingGame;

    [SerializeField] private GameSceneSO        _mainMenu             = default;
    [SerializeField] private LoadEventChannelSO _menuLoadEventChannel = default;


    [SerializeField] private UIInputSO          _UIInputReader     = default;
    [SerializeField] private VoidEventChannelSO _pauseEventChannel = default;

    private void Awake()
    {
        _quittingToMainMenu = _toMainMenuGO.GetComponent<QuittingWindow>();
        _quittingGame = _toQuitGO.GetComponent<QuittingWindow>();
    }

    private void OnEnable()
    {
        _onSceneReadyChannel.OnEventRaised += ResetUi;
        _pauseEventChannel.OnEventRaised   += OnPause;
    }

    private void OnDisable()    
    {
        // ResetUi();
        _onSceneReadyChannel.OnEventRaised -= ResetUi;
        _pauseEventChannel.OnEventRaised   -= OnPause;
    }

    private void OnPause()
    {
        _UIInputReader.gameOnPause = true;

        Time.timeScale = 0;

        _inGameMenu.ResumeClicked         += OnUnpause;
        _inGameMenu.Closed                += OnUnpause;
        _inGameMenu.QuitGameClicked       += ShowQuitGameModelWindow;
        _inGameMenu.BackToMainMenuClicked += ShowMainMenuModelWindow;
        _quittingToMainMenu.onConfirmEvent.AddListener(OnBackToMainMenu);
        _quittingGame.onConfirmEvent.AddListener(OnQuit);


        // _inGameMenu.gameObject.SetActive(true);


        GameInputSingeltone.GameInput.EnableMenuInput();
    }

    private void OnUnpause()
    {
        _UIInputReader.gameOnPause = false;

        _inGameMenu.ResumeClicked         -= OnUnpause;
        _inGameMenu.Closed                -= OnUnpause;
        _inGameMenu.QuitGameClicked       -= ShowQuitGameModelWindow;
        _inGameMenu.BackToMainMenuClicked -= ShowMainMenuModelWindow;
        _quittingToMainMenu.onConfirmEvent.RemoveListener(OnBackToMainMenu);
        _quittingGame.onConfirmEvent.RemoveListener(OnQuit);

        // _inGameMenu.gameObject.SetActive(false);

        Time.timeScale = 1;

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
        _UIInputReader.gameOnPause = false;
        _inGameMenu.gameObject.SetActive(false);
        _optionsPanel.SetActive(false);
        _hud.SetActive(true);

        Time.timeScale = 1;
    }

    private void ShowMainMenuModelWindow()
    {
        _toMainMenuGO.SetActive(true);
    }

    private void ShowQuitGameModelWindow()
    {
        _toQuitGO.SetActive(true);
    }
}