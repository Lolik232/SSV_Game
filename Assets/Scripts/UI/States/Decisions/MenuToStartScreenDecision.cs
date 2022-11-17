using FSM;

using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Menu To Start Screen Decision")]
public class MenuToStartScreenDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return stateMachine.UIInputSO.escPressed && !stateMachine.UIInputSO.optionsPressed;
    }
}