using System.Collections;
using System.Collections.Generic;
using FSM;
using UnityEngine;

[CreateAssetMenu(menuName = "FSM/Actions/Pause Options Action")]
public class PauseOptionsAction : FSMAction
{
    public override void OnEnter(BaseStateMachine stateMachine)
    {
        // stateMachine.settingsManager.Setup();

        stateMachine.optionsPanel.SetActive(true);

        stateMachine.gameVolumeSlider.Select();
    }

    public override void OnExit(BaseStateMachine stateMachine)
    {
        stateMachine.UIInputSO.optionsPressed = false;
        stateMachine.UIInputSO.escPressed = false;
        stateMachine.UIInputSO.gameOnPause = true;


        stateMachine.optionsPanel.SetActive(false);

        // stateMachine.settingsManager.Reset();
    }

    public override void Execute(BaseStateMachine stateMachine)
    {
    }
}