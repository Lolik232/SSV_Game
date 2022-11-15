using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Options Action")]
public class OptionsAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.menuAnim.SetBool("isButtonPressed", true);
        stateMachine.optionsAnim.SetBool("isButtonPressed", true);

        stateMachine.optionsGroup.interactable = true;

    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.UIInputSO.optionsPressed = false;
        stateMachine.UIInputSO.escPressed = false;

        stateMachine.optionsGroup.interactable = false;

        stateMachine.optionsAnim.SetBool("isButtonPressed", false);
        stateMachine.menuAnim.SetBool("isButtonPressed", false);
    }

    public override void Execute(BaseStateMachine stateMachine)
    {

    }
}