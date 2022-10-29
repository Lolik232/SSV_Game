using System;
using System.Collections.Generic;

public abstract class StaticManagerSO<T> : EntityComponentSO
{
	[NonSerialized] protected List<T> elements = new();
}
