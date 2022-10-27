using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManagerSO<T> : EntityComponentSO
{
	[NonSerialized] protected List<T> elements = new();
}
