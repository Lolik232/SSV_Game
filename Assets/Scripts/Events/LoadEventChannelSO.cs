using UnityEngine;

namespace All.Events
{
	[CreateAssetMenu(menuName = "Events/Load Event Channel")]
	public class LoadEventChannelSO : TypedEventChannelSO<GameSceneSO, bool, bool>
	{
		public new void RaiseEvent(GameSceneSO scene, bool showLoadingScreen = false, bool fadeScreen = false)
		{
			base.RaiseEvent(scene, showLoadingScreen, fadeScreen);
		}
	}
}