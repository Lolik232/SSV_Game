using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    [SerializeField] private Button _resumeButton         = default;
    [SerializeField] private Button _backToMainMenuButton = default;
    [SerializeField] private Button _quitGameButton       = default;

    [SerializeField] private UIInputSO _input;

    public event UnityAction ResumeClicked         = default;
    public event UnityAction BackToMainMenuClicked = default;
    public event UnityAction QuitGameClicked       = default;
    public event UnityAction Closed                = default;

    private void OnEnable()
    {
        _resumeButton.onClick.AddListener(Resume);
        _backToMainMenuButton.onClick.AddListener(BackToMainMenu);
        _quitGameButton.onClick.AddListener(QuitGame);
    }

    private void OnClose()
    {
        Closed?.Invoke();
    }

    private void OnDisable()
    {
        Resume();
        _resumeButton.onClick.RemoveListener(Resume);
        _backToMainMenuButton.onClick.RemoveListener(BackToMainMenu);
        _quitGameButton.onClick.RemoveListener(QuitGame);
    }

    private void Resume()
    {
        ResumeClicked?.Invoke();
    }

    private void BackToMainMenu()
    {
        BackToMainMenuClicked?.Invoke();
    }

    private void QuitGame()
    {
        QuitGameClicked?.Invoke();
    }
}