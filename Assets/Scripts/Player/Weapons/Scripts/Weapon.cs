using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
	[SerializeField] private string _weaponName;
	[SerializeField] private GameObject _baseOrigin;

	private bool _isActive;
	protected bool needExit;

	protected SpriteRenderer Sr
	{
		get; private set;
	}
	protected Animator BaseAnim
	{
		get; private set;
	}
	protected Animator Anim
	{
		get; private set;
	}
	protected Player Player
	{
		get; private set;
	}

	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();

	protected virtual void Awake()
	{
		Player = _baseOrigin.GetComponent<Player>();
		BaseAnim = _baseOrigin.GetComponent<Animator>();
		Anim = GetComponentInChildren<Animator>();
		Sr = GetComponentInChildren<SpriteRenderer>();
	}

	protected virtual void Start()
	{
		_isActive = false;
		needExit = false;

		updateActions.Clear();
		enterActions = new List<UnityAction> { ()=>
				{
						BaseAnim.SetBool(_weaponName, true);
						Anim.SetBool("attack", true);
						_isActive = true;
						needExit = false;
				} };
		exitActions = new List<UnityAction> { ()=>
				{
						BaseAnim.SetBool(_weaponName, false);
						Anim.SetBool("attack", false);
						_isActive = false;
				} };
	}

	public void OnEnter()
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

	public void OnExit()
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
				OnExit();
			}
		}
	}

	protected virtual void OnDrawGizmos()
	{

	}
}
