using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class AbilitiesManager : MonoBehaviour
{
	private List<Ability> _abilities = new();

	private void Awake()
	{
		GetComponents(_abilities);
	}

	private void Update()
	{
		StartCoroutine(WaitForChecks());
	}

	private IEnumerator WaitForChecks()
	{
		yield return new WaitForFixedUpdate();
		foreach (var ability in _abilities)
		{
			ability.TryEnter();
			ability.OnUpdate();
		}
	}
}