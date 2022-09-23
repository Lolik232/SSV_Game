using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMoveToIdleCondition", menuName = "Player/States/Conditions/Move To Idle")]
public class PlayerMoveToIdleCondition : ConditionSO
{
    [SerializeField] private PlayerInputReader _inputReader;

    private Vector2Int _moveInput;

    private void OnMove(Vector2Int value) => _moveInput = value;

    private void OnEnable()
    {
        _inputReader.MoveEvent += OnMove;
    }

    private void OnDisable()
    {
        _inputReader.MoveEvent -= OnMove;
    }

    public override bool GetStatement()
    {
        return _moveInput.x == 0;
    }
}
