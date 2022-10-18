using System;

using UnityEngine;

[Serializable]
public class Parameter
{
	[SerializeField] private float _min;
	[SerializeField] private float _max;
	private float _current;

	[NonSerialized] public float preAddition;
	[NonSerialized] public float multiplier = 1;
	[NonSerialized] public float postAddition;

	public float Min => _min;
	public float Max => (_max + preAddition) * multiplier + postAddition;
	public float Current
	{
		get => _current;
		set => Set(value);
	}

	public void Set(float value)
	{
		_current = Mathf.Max(Min, Mathf.Min(Max, value));
	}

	public static implicit operator float(Parameter param) => param.Current;
}
