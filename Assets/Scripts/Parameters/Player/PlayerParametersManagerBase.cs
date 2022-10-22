using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametersManagerBase : BaseMonoBehaviour
{
	[SerializeField] private PlayerParametersManagerSO _parametersManager;

	protected override void Awake()
	{
		_parametersManager.Initialize();

		base.Awake();
	}
}
