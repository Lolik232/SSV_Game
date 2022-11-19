using FSM;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "FSM/Actions/Game Action")]
public class GameAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        Time.timeScale = 1f;
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}