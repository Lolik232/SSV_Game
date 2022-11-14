using System.Collections.Generic;

using UnityEngine;

namespace FSM
{
	public class BaseStateMachine : MonoBehaviour
	{
		[SerializeField] private BaseState _initialState;
		[SerializeField] private UIInputSO _uiInputSO;
		[SerializeField] private Animator _textAnim;
		[SerializeField] private Animator _menuAnim;
		[SerializeField] private Animator _optionsAnim;
		[SerializeField] private CanvasGroup _menuGroup;
		[SerializeField] private CanvasGroup _optionsGroup;
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