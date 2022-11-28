using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class DeadState : State
{
    private Rigidbody2D _rb;

    protected override void Awake()
    {
        base.Awake();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnFail()
    {
        Entity.enabled = false;
        _rb.velocity = new Vector2(0f, _rb.velocity.y);
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}
