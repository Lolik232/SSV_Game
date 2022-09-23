using System.Collections;
using System.Collections.Generic;

using All.Events;

using UnityEngine;

[CreateAssetMenu(fileName = "PlayerXMoveAction", menuName = "Player/States/Actions/X Move")]

public class PlayerXMoveActionSO : ActionSO
{
    [SerializeField] private PlayerInputReader _inputReader;

    [SerializeField] private IntEventChannelSO _xMoveWriter;

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

    public override void Apply()
    {
        _xMoveWriter.RaiseEvent(_moveInput.x);
    }
}
