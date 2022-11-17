using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Start Screen Action")]
public class StartScreenAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.menuAnim.SetBool("isTextHidden", false);
        stateMachine.textAnim.SetBool("isTextHidden", false);
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {

    }

    public override void Execute(BaseStateMachine stateMachine)
    {

    }
}
