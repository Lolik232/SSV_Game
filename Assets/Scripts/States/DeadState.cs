public class DeadState : State
{
    private void OnDead()
    {
        Destroy(gameObject);
    }
}
