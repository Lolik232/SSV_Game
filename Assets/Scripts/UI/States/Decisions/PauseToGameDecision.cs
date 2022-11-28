using FSM;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Pause To Game")]
public class PauseToGameDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return stateMachine.UIInputSO.escPressed || stateMachine.UIInputSO.gameOnPause == false;
    }
}