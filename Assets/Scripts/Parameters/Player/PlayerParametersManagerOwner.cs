using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParametersManagerOwner : MonoBehaviour
{
	[SerializeField] private PlayerParametersManagerSO _parametersManager;

	private void Start()
	{
		_parametersManager.Initialize();
	}
}
