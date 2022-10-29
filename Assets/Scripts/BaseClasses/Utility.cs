using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public static class Utility
{
	static public void ApplyActions(List<UnityAction> actions)
	{
		foreach (var action in actions)
		{
			action();
		}
	}

	static public void ApplyActions<T>(List<UnityAction<T>> actions, T value)
	{
		foreach (var action in actions)
		{
			action(value);
		}
	}

	static public void SetAnimBoolsOnEnter(Animator anim, List<AnimationBool> animBools)
	{
		foreach (var animBool in animBools)
		{
			anim.SetBool(animBool.name, animBool.onEnterValue);
		}
	}

	static public void SetAnimBoolsOnExit(Animator anim, List<AnimationBool> animBools)
	{
		foreach (var animBool in animBools)
		{
			if (animBool.onExitValue != animBool.onEnterValue)
			{
				anim.SetBool(animBool.name, animBool.onExitValue);
			}
		}
	}

	static public void SetAnimTrigger(Animator anim, List<string> names)
	{
		foreach (var name in names)
		{
			anim.SetTrigger(name);
		}
	}

	static public void BlockAll(List<BlockedAbility> blockedAbilities)
	{
		foreach (var blockedAbility in blockedAbilities)
		{
			blockedAbility.component.Block(blockedAbility.needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedAbility> blockedAbilities)
	{
		foreach (var blockedAbility in blockedAbilities)
		{
			blockedAbility.component.Unlock();
		}
	}

	static public void BlockAll(List<BlockedState> blockedStates)
	{
		foreach (var blockedState in blockedStates)
		{
			blockedState.component.SetBlockedTransition(blockedState.target);
			blockedState.component.Block(blockedState.needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedState> blockedStates)
	{
		foreach (var blockedState in blockedStates)
		{
			blockedState.component.Unlock();
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
