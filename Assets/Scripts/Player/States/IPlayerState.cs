using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState : IState
{
    void InitializePlayer(Player player);
}
