using System.Collections;
using UnityEngine;

public class DeadState : State
{
    protected override void ApplyEnterActions()
    {
        base.ApplyEnterActions();
        StartCoroutine(ApplyDeath());
    }

    private IEnumerator ApplyDeath()
    {
        yield return new WaitForSeconds(2);

        Destroy(gameObject);
    }
}
