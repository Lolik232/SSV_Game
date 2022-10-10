using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerAttackState", menuName = "Player/States/Ability States/Attack")]
public class PlayerAttackStateSO : PlayerAbilityStateSO
{
    [Header("Connected Ability")]
    [SerializeField] private PlayerAttackAbilitySO _attackAbility;

    protected override void OnEnable()
    {
        base.OnEnable();
        enterActions.Add(() =>
        {
            Player.SetVelocityZero();
            Player.CheckIfShouldFlip(Player.attackDirection.x >= 0 ? 1 : -1);
        });

        animationActions.Add((int index) =>
        {
            switch (index)
            {
                case 0:
                    Player.Weapon.enabled = true;
                    break;
                case 1:
                    Player.Weapon.enabled = false;
                    _attackAbility.isActive = false;
                    break;
            }
        });

        animationFinishActions.Add(() =>
        {
            
            abilityDone = true;
        });
    }
}
