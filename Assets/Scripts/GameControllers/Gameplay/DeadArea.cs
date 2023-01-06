using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadArea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out var damageable))
        {
            damageable.TakeDamage(damageable.Health,
                                  transform.position);
        }
    }
}