public class PlayerAbilitySO : AbilitySO
{
	protected Player Player
	{
		get; private set;
	}

	public void Initialize(Player player)
	{
		InitializeAnimator(player.Anim);
		Player = player;
	}
}
