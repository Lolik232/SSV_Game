using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParametersManagerBase : BaseMonoBehaviour
{
	[SerializeField] private ParametersManagerSO _parametersManager;

	protected override void Awake()
	{
		_parametersManager.Initialize();

		base.Awake();
	}
}
