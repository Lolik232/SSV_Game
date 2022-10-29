using System.Collections.Generic;
using UnityEngine;

public class Cacheable<T> : ICacheable<T>
{
	private readonly List<CachedValue<T>> _cache = new();
	private readonly Blocker _blocker = new();

	private int _last;

	private T _value;

	private int Count
	{
		get
		{
			return _last + 1;
		}
	}

	public T Value
	{
		get
		{
			return _value;
		}
	}

	public int Hold(T value)
	{
		if (_cache.Capacity < Count + 1)
		{
			_cache.Capacity *= 2;
		}

		_last++;
		_cache[_last] = new CachedValue<T>(value, _last);
		_value = value;
		_blocker.AddBlock();

		return _last;
	}

	public void Release(int id)
	{
		int GetLast(int last)
		{
			while (last != _cache[last].place)
			{
				last = _cache[last].place;
			}

			return last;
		}

		if (id == _last)
		{
			_value = _cache[id].value;
		}

		_cache[id].SetPlace(id > 0 ? GetLast(id - 1) : 0);
		_last = GetLast(_last);

		_blocker.RemoveBlock();
	}

	public bool TrySet(T value)
	{
		if (!_blocker.IsLocked)
		{
			_value = value;
			return true;
		}
		else
		{
			_cache[0].SetValue(value);
			return false;
		}
	}
}

public struct CachedValue<T>
{
	public T value;
	public int place;

	public CachedValue(T value, int place)
	{
		this.value = value;
		this.place = place;
	}

	public void SetValue(T value)
	{
		this.value = value;
	}

	public void SetPlace(int place)
	{
		this.place = place;
	}
}