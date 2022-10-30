using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.HID;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace FSM
{
	public class BaseStateMachine : MonoBehaviour
	{
		[SerializeField] private BaseState _initialState;
		[SerializeField] private UIInputSO _uiInputSO;
		[SerializeField] private Animator _textAnim;
		[SerializeField] private Animator _menuAnim;
		[SerializeField] private List<Button> _buttons;

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

		public List<Button> buttons => _buttons;

		private void Update()
		{
			CurrentState.Execute(this);
		}

		public void ChangeState(BaseState newState)
		{
			CurrentState = newState;

			CurrentState.OnEnter(this);
		}
	}
}