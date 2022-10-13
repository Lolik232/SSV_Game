using UnityEngine;

namespace All.Events
{
	[CreateAssetMenu(menuName = "Events/Fade Event Channel")]
	public class FadeChannelSO : TypedEventChannelSO<bool, float, Color>
	{
		public void RaiseEvent(float duration)
		{
			RaiseEvent(false, duration, Color.black);
		}

		public override void RaiseEvent(bool fadeIn, float duration, Color color)
		{
			base.RaiseEvent(fadeIn, duration, color);
		}
	}
}