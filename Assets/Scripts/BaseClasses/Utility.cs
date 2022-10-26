using System;
using System.Collections;
using System.Collections.Generic;

using Mono.CompilerServices.SymbolWriter;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.ResourceProviders.Simulation;

using static UnityEngine.Rendering.DebugUI;

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

	static public void SetAnimBools(Animator anim, List<string> names, bool value)
	{
		foreach (var name in names)
		{
			anim.SetBool(name, value);
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
		foreach (var ability in blockedAbilities)
		{
			ability.component.Block(ability.needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedAbility> blockedAbilities)
	{
		foreach (var ability in blockedAbilities)
		{
			ability.component.Unlock();
		}
	}

	static public void BlockAll(List<BlockedState> blockedStates)
	{
		foreach (var state in blockedStates)
		{
			state.component.SetBlockedTransition(state.target);
			state.component.Block(state.needHardExit);
		}
	}

	static public void UnlockAll(List<BlockedState> blockedStates)
	{
		foreach (var state in blockedStates)
		{
			state.component.Unlock();
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
