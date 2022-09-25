using System.Collections;
using System.Collections.Generic;

using TMPro.EditorUtilities;

using UnityEngine;

public class PlayerSubStateSO : SubStateSO, IPlayerState
{
    protected Player Player { get; private set; }

    public virtual void InitializePlayer(Player player)
    {
        Player = player;
    }
}
