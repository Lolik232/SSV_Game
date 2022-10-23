using UnityEngine;

namespace FSM
{
	public class BaseStateMachine : MonoBehaviour
	{
		[SerializeField] private BaseState _initialState;
		[SerializeField] private UIInput _uiInput;
		[SerializeField] private Animator _textAnim;
		[SerializeField] private Animator _menuAnim;

		private void Awake()
		{
			CurrentState = _initialState;
			UIInput = _uiInput;
			menuAnim = _menuAnim;
			textAnim = _textAnim;
		}

		public BaseState CurrentState
		{
			get;
			set;
		}

		public UIInput UIInput
		{
			get;
			private set;
		}

		public Animator menuAnim
		{
			get;
			private set;
		}

		public Animator textAnim
		{
			get;
			private set;
		}

		private void Update()
		{
			CurrentState.Execute(this);
		}
	}
}