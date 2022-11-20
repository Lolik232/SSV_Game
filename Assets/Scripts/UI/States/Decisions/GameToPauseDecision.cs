using FSM;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Decisions/Game To Pause")]
public class GameToPauseDecision : Decision
{
    public override bool Decide(BaseStateMachine stateMachine)
    {
        return stateMachine.UIInputSO.gameOnPause;
    }
}