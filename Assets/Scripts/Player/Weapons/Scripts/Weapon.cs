using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
	[SerializeField] private string _weaponName;
	[SerializeField] private GameObject _base;
	[SerializeField] private GameObject _hit;

	protected PlayerInputReaderSO inputReader;

	private bool _isActive;

	protected bool needExit;
	protected bool isDirectionHoldOn;
	protected int heldDirection;

	protected Animator baseAnim;
	protected Animator anim;
	protected Transform hitPos;
	protected SpriteRenderer hitSr;
	protected Player player;

	protected List<UnityAction> updateActions = new();
	protected List<UnityAction> enterActions = new();
	protected List<UnityAction> exitActions = new();

	protected virtual void Awake()
	{
		player = _base.GetComponent<Player>();
		baseAnim = _base.GetComponent<Animator>();
		inputReader = _base.GetComponent<PlayerInputReaderOwner>().inputReader;
		anim = GetComponentInChildren<Animator>();
		hitPos = _hit.GetComponent<Transform>();
		hitSr = _hit.GetComponent<SpriteRenderer>();
	}

	protected virtual void Start()
	{
		_isActive = false;
		needExit = false;

		updateActions.Clear();
		enterActions = new List<UnityAction> { ()=>
			{
					baseAnim.SetBool(_weaponName, true);
					anim.SetBool("attack", true);
					_isActive = true;
					needExit = false;
			} };
		exitActions = new List<UnityAction> { ()=>
			{
					baseAnim.SetBool(_weaponName, false);
					anim.SetBool("attack", false);
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

	public void Update()
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

	public void HoldDirection(int direction)
	{
		isDirectionHoldOn = true;
		heldDirection = direction;
	}

	public void ReleaseDirection()
	{
		isDirectionHoldOn = false;
	}

	protected virtual void OnDrawGizmos()
	{

	}
}
