using System.Collections.Generic;

using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
	private readonly List<Ability> _abilities = new();

	private void Awake()
	{
		GetComponents(_abilities);
	}

	private void Update()
	{
		foreach (var ability in _abilities)
		{
			ability.TryEnter();
			ability.OnUpdate();
		}
	}
}