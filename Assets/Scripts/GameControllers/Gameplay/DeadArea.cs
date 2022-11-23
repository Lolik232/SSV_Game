using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Entity entity = collision.GetComponent<Entity>();

        if (entity is IDamageable)
        {
            var damageable = entity as IDamageable;
            damageable.OnDead();
        }
    }
}
