public class PlayerState : State
{
    protected Player Player
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Player = GetComponent<Player>();
    }
}
