using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public static class Utility
{

	static public bool CheckAll(List<Condition> conditions)
	{
		foreach (var condition in conditions)
		{
			if (!condition.DoChecks())
			{
				return false;
			}
		}

		return true;
	}

	static public void ConnectComponent<T>(ref T component, GameObject origin) where T : MonoBehaviour
	{
		if (component is null)
		{
			component = origin.GetComponent<T>();
		}
	}

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
			switch (animBool.mode)
			{
				case AnimationBool.EnableMode.Enable:
				case AnimationBool.EnableMode.EnableOnEnterDisableOnExit:
					anim.SetBool(animBool.name, true);
					break;
				case AnimationBool.EnableMode.Disable:
				case AnimationBool.EnableMode.DisableOnEnterEnableOnExit:
					anim.SetBool(animBool.name, false);
					break;
			}
		}
	}

	static public void SetAnimBoolsOnExit(Animator anim, List<AnimationBool> animBools)
	{
		foreach (var animBool in animBools)
		{
			switch (animBool.mode)
			{
				case AnimationBool.EnableMode.Disable:
				case AnimationBool.EnableMode.EnableOnEnterDisableOnExit:
					anim.SetBool(animBool.name, false);
					break;
				case AnimationBool.EnableMode.Enable:
				case AnimationBool.EnableMode.DisableOnEnterEnableOnExit:
					anim.SetBool(animBool.name, true);
					break;
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

	static public void BlockAll(List<PermitedAbility> blockedAbilities)
	{
		foreach (var blockedAbility in blockedAbilities)
		{
			blockedAbility.component.Block();
		}
	}

	static public void UnlockAll(List<PermitedAbility> blockedAbilities)
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
			blockedState.component.Block();
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
}
