using System.Collections;
using System.Collections.Generic;
using All.Events;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStandAction", menuName = "Player/States/Actions/Stand")]

public class PlayerStandActionSO : ActionSO
{
    [SerializeField] private IntEventChannelSO _xMoveWriter;

    public override void Apply()
    {
        _xMoveWriter.RaiseEvent(0);
    }
}
