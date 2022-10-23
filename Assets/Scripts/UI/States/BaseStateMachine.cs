using UnityEngine;
using UnityEngine.Serialization;

namespace FSM
{
	public class BaseStateMachine : MonoBehaviour
	{
		[SerializeField] private BaseState _initialState;
		[SerializeField] private UIInputSO _uiInputSO;
		[SerializeField] private Animator _textAnim;
		[SerializeField] private Animator _menuAnim;

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

		private void Update()
		{
			CurrentState.Execute(this);
		}
	}
}