using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametersManagerBase : MonoBehaviour
{
	[SerializeField] private PlayerParametersManagerSO _parametersManager;

	private void Start()
	{
		_parametersManager.Initialize();
	}
}
