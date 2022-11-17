using System.Collections;

using UnityEngine;

public class DeadState : State
{
    private void OnDead()
    {
        Destroy(gameObject);
    }
}
