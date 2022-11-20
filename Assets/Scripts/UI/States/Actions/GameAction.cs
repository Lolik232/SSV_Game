using FSM;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "FSM/Actions/Game Action")]
public class GameAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.pause.SetActive(false);
        stateMachine.pausePanel.SetActive(false);
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        // stateMachine.pause.SetActive(true);
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}