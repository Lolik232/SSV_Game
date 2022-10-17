using System;
using System.Collections.Generic;
using System.Linq;

public class Cacher<T>
{
	private readonly Blocker _blocker = new();
	private readonly Dictionary<int, T> _cache = new();
	private readonly List<int> _ids = new();

	private T _savedValue;

	private T _value;

	private int _lastId;

	public Cacher(T value)
	{
		_value = value;
	}

	public bool TrySet(T value)
	{
		_savedValue = value;
		if (!_blocker.IsLocked)
		{
			_value = value;
		}

		return !_blocker.IsLocked;
	}

	public int Hold(T value)
	{
		if (_cache.Count > 0)
		{
			_lastId = _ids[^1] + 1;
		}
		else
		{
			_lastId = 0;
			_savedValue = _value;
		}

		_ids.Add(_lastId);
		_cache.Add(_lastId, _value);
		_value = value;
		_blocker.AddBlock();

		return _lastId;
	}

	public void Release(int id)
	{
		_blocker.RemoveBlock();
		int i = _ids.IndexOf(id);
		if (id == _lastId)
		{
			_value = (i > 0) ? _cache.GetValueOrDefault(i - 1) : _savedValue;
		}

		_ids.RemoveAt(i);
		_cache.Remove(id);

		if (_cache.Count > 0)
		{
			_lastId = _ids[^1];
		}
	}

	public static implicit operator T(Cacher<T> cachingValue) => cachingValue._value;
}
