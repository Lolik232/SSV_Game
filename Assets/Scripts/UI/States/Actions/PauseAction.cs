using FSM;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "FSM/Actions/Pause Action")]
public class PauseAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.pausePanel.SetActive(true);
        stateMachine.resumeButton.Select();

        Time.timeScale = 0f;
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.pausePanel.SetActive(false);

        Time.timeScale = 1f;
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}