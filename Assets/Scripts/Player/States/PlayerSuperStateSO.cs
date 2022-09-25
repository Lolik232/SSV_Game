using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class PlayerSuperStateSO : SuperStateSO, IPlayerState
{
    protected Player Player { get; private set; }

    public void InitializePlayer(Player player)
    {
        Player = player;
        foreach (var subState in subStates.Cast<PlayerSubStateSO>())
        {
            subState.InitializePlayer(player);
        }
    }

}
