using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviourControllerSO : EntityComponentSO
{
	[NonSerialized] public Vector2Int move;

	[NonSerialized] public Vector2 lookAtPosition;
	[NonSerialized] public Vector2 lookAtDirection;
	[NonSerialized] public float   lookAtDistance;
}
