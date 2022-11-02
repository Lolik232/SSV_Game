using System;

using UnityEngine;

[Serializable]
public class Parameter : IParameter<float>
{
	[SerializeField] private float _min;
	[SerializeField] private float _max;

	private float _addition;
	private float _multiplier;
	private float _afterAddition;

	private float _current;

	public float Min
	{
		get => _min;
	}
	public float Max
	{
		get => _max;
	}
	public float Addition
	{
		get => _addition;
		set => _addition = value;
	}
	public float Multiplier
	{
		get => _multiplier;
		set => _multiplier = value;
	}
	public float AfterAddition
	{
		get => _afterAddition;
		set => _afterAddition = value;
	}
	public float Current
	{
		get => _current;
		set => _current = value;
	}

	public void Initialize()
	{
		Current = Max;
	}

	public void Set(float value)
	{
		Current = Mathf.Min(Min, Mathf.Max(Max, value));
	}
}

[Serializable]
public class ParameterInt : IParameter<int>
{
	[SerializeField] private int _min;
	[SerializeField] private int _max;

	private int _addition;

	private int _current;

	public int Min
	{
		get => _min;
	}
	public int Max
	{
		get => _max;
	}
	public int Addition
	{
		get => _addition;
		set => _addition = value;
	}
	public int Current
	{
		get => _current;
		set => _current = value;
	}

	public void Initialize()
	{
		Current = Max;
	}

	public void Set(int value)
	{
		Current = Mathf.Min(Min, Mathf.Max(Max, value));
	}
}

