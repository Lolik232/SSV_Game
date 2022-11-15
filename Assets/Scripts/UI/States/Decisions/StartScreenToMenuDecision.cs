using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Start Screen To Menu")]
public class StartScreenToMenuDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return stateMachine.UIInputSO.enterPressed;


    }
}