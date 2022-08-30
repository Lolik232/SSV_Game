using System;

public interface IPlayerState
{
    event Action EnterEvent;
    event Action ExitEvent;

    void Enter();
    void Exit();
    void InputUpdate();
    void LogicUpdate();
    void PhysicsUpdate();
}