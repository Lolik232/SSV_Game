using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.Events;

public abstract class WeaponSO : ScriptableObject
{
	[SerializeField] private string _weaponName;

	[SerializeField] protected PlayerDataSO data;

	private bool _isActive;

	protected bool needExit;

	protected Animator baseAnim;
	protected Animator anim;
	protected Hit hit;

	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();

	protected readonly Blocker directionBlocker = new();

	protected int holdDirection;

	protected void InitializeBaseAnimator(Animator baseAnim) => this.baseAnim = baseAnim;
	protected void InitializeAnimator(Animator anim) => this.anim = anim;
	protected void InitializeHit(Hit hit) => this.hit = hit;

	protected virtual void OnEnable()
	{
		_isActive = false;
		needExit = false;

		updateActions.Clear();
		enterActions = new List<UnityAction>
		{ ()=>
			{
					baseAnim.SetBool(_weaponName, true);
					anim.SetBool("attack", true);
					_isActive = true;
					needExit = false;
			} };
		exitActions = new List<UnityAction>
		{ ()=>
			{
					baseAnim.SetBool(_weaponName, false);
					anim.SetBool("attack", false);
					_isActive = false;
			} };
	}

	public void OnWeaponEnter()
	{
		if (_isActive)
		{
			return;
		}

		foreach (var action in enterActions)
		{
			action();
		}
	}

	public void OnWeaponExit()
	{
		if (!_isActive)
		{
			return;
		}

		foreach (var action in exitActions)
		{
			action();
		}
	}

	public void OnUpdate()
	{
		foreach (var action in updateActions)
		{
			if (!_isActive)
			{
				return;
			}

			action();
			if (needExit)
			{
				OnWeaponExit();
			}
		}
	}

	public void HoldDirection(int direction)
	{
		holdDirection = direction;
		directionBlocker.AddBlock();
	}

	public void ReleaseDirection()
	{
		directionBlocker.RemoveBlock();
	}

	public virtual void OnDrawGizmos()
	{
	}
}
