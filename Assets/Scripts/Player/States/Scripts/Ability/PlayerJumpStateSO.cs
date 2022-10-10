using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerJumpState", menuName = "Player/States/Ability States/Jump")]

public class PlayerJumpStateSO : PlayerAbilityStateSO
{
    [Header("Connected Ability")]
    [SerializeField] private PlayerJumpAbilitySO _jumpAbility;

    protected override void OnEnable()
    {
        base.OnEnable();

        enterActions.Add(() =>
        {
            abilityDone = true;
            isGroundedTransitionBlock = true;
            Player.SetVelocityY(_jumpAbility.Force);
        });
    }
}
