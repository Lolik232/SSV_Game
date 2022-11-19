using System.Collections.Generic;
using Systems.SaveSystem;
using Systems.SaveSystem.Settings.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FSM
{
    public class BaseStateMachine : MonoBehaviour
    {
        [SerializeField] private BaseState   _initialState;
        
        [SerializeField] private UIInputSO   _uiInputSO;
        
        [SerializeField] private Animator    _textAnim;
        [SerializeField] private Animator    _menuAnim;
        [SerializeField] private Animator    _optionsAnim;
        
        [SerializeField] private CanvasGroup _menuGroup;
        [SerializeField] private CanvasGroup _optionsGroup;

        [SerializeField] private Button _newGameButton;
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Slider _gameVolumeSlider;

        [FormerlySerializedAs("_continueButton")] [SerializeField] public GameObject     continueButton;
        [SerializeField] public GameObject pausePanel;
        [SerializeField] public GameObject optionsPanel;

        
        [SerializeField] public SaveSystem _saveSystem = default;

        [SerializeField] public UISettingsManager settingsManager;

        private void Awake()
        {
            CurrentState = _initialState;
        }

        public BaseState CurrentState
        {
            get;
            set;
        }

        public UIInputSO UIInputSO => _uiInputSO;

        public Animator menuAnim => _menuAnim;

        public Animator textAnim => _textAnim;

        public Animator optionsAnim => _optionsAnim;

        public CanvasGroup menuGroup => _menuGroup;

        public CanvasGroup optionsGroup => _optionsGroup;

        public Button newGameButton => _newGameButton;
        
        public Button resumeButton => _resumeButton;

        public Slider gameVolumeSlider => _gameVolumeSlider;

        private void Update()
        {
            CurrentState.Execute(this);
        }

        public void ChangeState(BaseState newState)
        {
            CurrentState.OnExit(this);

            CurrentState = newState;

            CurrentState.OnEnter(this);
        }
    }
}