using FSM;

using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "FSM/Actions/Menu Action")]
public class MenuAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        stateMachine.textAnim.SetBool("isTextHidden", true);
        stateMachine.menuAnim.SetBool("isTextHidden", true);

        stateMachine.menuGroup.interactable = true;

        if (stateMachine._saveSystem.HasSave())
        {
            stateMachine.continueButton.SetActive(true);
            var button = stateMachine.continueButton.GetComponentInChildren<Button>();
            button.Select();
        }
        else
        {
            stateMachine.continueButton.SetActive(false);
            stateMachine.newGameButton.Select();
        }
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.menuGroup.interactable = false;
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}