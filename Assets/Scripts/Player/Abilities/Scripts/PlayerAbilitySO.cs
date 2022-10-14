public class PlayerAbilitySO : AbilitySO
{
	protected PlayerInputReaderSO inputReader;
	protected Player player;

	public void Initialize(Player player)
	{
		InitializeAnimator(player.anim);
		this.player = player;
		inputReader = player.inputReader;
	}
}
