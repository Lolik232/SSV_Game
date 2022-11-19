public class DeadState : State
{
    private void OnFail()
    {
        Entity.enabled = false;
    }

    private void OnDead()
    {
        Destroy(gameObject);
    }
}
