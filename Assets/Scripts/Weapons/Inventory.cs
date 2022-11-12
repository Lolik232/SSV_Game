using System.Collections.Generic;

using UnityEngine;

public class Inventory : MonoBehaviour
{
	private readonly List<Weapon> _weapons = new();

	private int _current;

	public Weapon Current
	{
		get => _weapons[_current];
	}

	private void Awake()
	{
		GetComponentsInChildren(_weapons);
	}

	private void Start()
	{
		_current = 0;
	}

	public void GetNext()
	{
		Current.OnExit();
		if (_current == _weapons.Count - 1)
		{
			_current = 0;
		}
		else
		{
			_current++;
		}
	}

	public void GetPrev()
	{
		Current.OnExit();
		if (_current == 0)
		{
			_current = _weapons.Count - 1;
		}
		else
		{
			_current--;
		}
	}
}
