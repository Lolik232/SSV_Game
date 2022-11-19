using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Menu Action")]
public class MenuAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.textAnim.SetBool("isTextHidden", true);
        stateMachine.menuAnim.SetBool("isTextHidden", true);

        stateMachine.menuGroup.interactable = true;

        stateMachine.continueButton.SetActive(stateMachine._saveSystem.HasSave());
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.menuGroup.interactable = false;
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}