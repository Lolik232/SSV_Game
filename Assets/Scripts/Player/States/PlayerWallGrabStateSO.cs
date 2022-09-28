using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerWallGrabState", menuName = "State Machine/States/Player/Sub States/Wall Grab")]

public class PlayerWallGrabStateSO : PlayerSubStateSO
{
    private Vector2 _holdPosition;

    [SerializeField] private PlayerWallSlideStateSO _toWallSlideState;

    protected override void OnEnable()
    {
        base.OnEnable();

        transitions.Add(new TransitionItem(_toWallSlideState, () => Player.moveInput.y < 0f || !Player.grabInput));

        enterActions.Add(() =>
        {
            _holdPosition = Player.transform.position;
            Player.HoldPosition(_holdPosition);
        });

        updateActions.Add(() => { Player.HoldPosition(_holdPosition); });
    }
}
