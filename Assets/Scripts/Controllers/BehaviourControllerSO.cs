using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourControllerSO : BaseScriptableObject
{
	[SerializeField] protected EntitySO entity;

	[NonSerialized] public bool jump;
	[NonSerialized] public bool grab;
	[NonSerialized] public bool attack;
	[NonSerialized] public bool ability;

	[NonSerialized] public Vector2Int move;

	[NonSerialized] public Vector2 lookAtPosition;
	[NonSerialized] public Vector2 lookAtDirection;
	[NonSerialized] public float   lookAtDistance;
}
