using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public static class Utility
{
	static public void ApplyActions(List<UnityAction> actions)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			actions[i]();
		}
	}

	static public void ApplyActions<T>(List<UnityAction<T>> actions, T value)
	{
		for (int i = 0; i < actions.Count; i++)
		{
			actions[i](value);
		}
	}

	static public void SetAnimBools(Animator anim, List<string> names, bool value)
	{
		for (int i = 0; i < names.Count; i++)
		{
			anim.SetBool(names[i], value);
		}
	}

	static public void SetAnimTrigger(Animator anim, List<string> names)
	{
		for (int i = 0; i < names.Count; i++)
		{
			anim.SetTrigger(names[i]);
		}
	}

	static public void BlockAll(List<BlockedAbility> blockedAbilities)
	{
		for (int i = 0; i < blockedAbilities.Count; i++)
		{
			blockedAbilities[i].component.Block(blockedAbilities[i].needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedAbility> blockedAbilities)
	{
		for (int i = 0; i < blockedAbilities.Count; i++)
		{
			blockedAbilities[i].component.Unlock();
		}
	}

	static public void BlockAll(List<BlockedState> blockedStates)
	{
		for (int i = 0; i < blockedStates.Count; i++)
		{
			blockedStates[i].component.SetBlockedTransition(blockedStates[i].target);
			blockedStates[i].component.Block(blockedStates[i].needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedState> blockedStates)
	{
		for (int i = 0; i < blockedStates.Count; i++)
		{
			blockedStates[i].component.Unlock();
		}
	}

	public static T Check<T>(Func<Vector2, Vector2, int, T> checkFunction, Tuple<Vector2, Vector2> points, LayerMask whatIsTarget)
	{
		return checkFunction(points.Item1, points.Item2, whatIsTarget);
	}

	public static T Check<T>(Func<Vector2, Vector2, int, T> checkFunction, Vector2 pointA, Vector2 pointB, LayerMask whatIsTarget)
	{
		return checkFunction(pointA, pointB, whatIsTarget);
	}

	static public void DrawArea(Tuple<Vector2, Vector2> ray)
	{
		var a = ray.Item1;
		var b = ray.Item2;

		Gizmos.DrawLine(a, b);
		Gizmos.DrawWireCube((a + b) / 2, new Vector2(Mathf.Max(a.x, b.x) - Mathf.Min(a.x, b.x), Mathf.Max(a.y, b.y) - Mathf.Min(a.y, b.y)));
	}

	static public void DrawLine(Tuple<Vector2, Vector2> ray)
	{
		Gizmos.DrawLine(ray.Item1, ray.Item2);
	}
}
