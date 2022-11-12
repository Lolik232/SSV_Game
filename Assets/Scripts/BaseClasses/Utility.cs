using UnityEngine;

public static class Utility
{
	static public void DrawArea(CheckArea area, bool detected, Color color)
	{
		var a = area.a;
		var b = area.b;

		Gizmos.color = SetTargetDetectedColor(detected, color);
		Gizmos.DrawLine(a, b);
		Gizmos.DrawWireCube((a + b) / 2, new Vector2(Mathf.Max(a.x, b.x) - Mathf.Min(a.x, b.x), Mathf.Max(a.y, b.y) - Mathf.Min(a.y, b.y)));
	}

	static public void DrawLine(CheckArea ray, bool detected, Color color)
	{
		Gizmos.color = SetTargetDetectedColor(detected, color);
		Gizmos.DrawLine(ray.a, ray.b);
	}

	static private Color SetTargetDetectedColor(bool detected, Color color)
	{
		if (detected)
		{
			return color;
		}
		else
		{
			return Color.black;
		}
	}

	static public void SetAnimationSpeed(Animator anim, string name, float duration)
	{
		string animationName = name.Remove(1).ToUpper() + name.Remove(0, 1);
		string speedName = name + "Speed";
		anim.SetFloat(speedName, 1f);

		AnimationClip[] clips = anim.runtimeAnimatorController.animationClips;
		foreach (var clip in clips)
		{
			if (clip.name == animationName)
			{
				anim.SetFloat(speedName, clip.length / duration);
				return;
			}
		}
	}

	static public Vector2 Rotate(this Vector2 v, float degrees)
	{
		return Quaternion.Euler(0f, 0f, degrees) * v;
	}
}
