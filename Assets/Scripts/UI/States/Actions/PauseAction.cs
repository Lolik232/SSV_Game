using FSM;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "FSM/Actions/Pause Action")]
public class PauseAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.pause.SetActive(true);
        stateMachine.pausePanel.SetActive(true);
        stateMachine.resumeButton.Select();
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.UIInputSO.escPressed  = false;
        stateMachine.UIInputSO.gameOnPause = true;    
        stateMachine.pausePanel.SetActive(false);
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}