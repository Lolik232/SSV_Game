using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ComponentSO : BaseScriptableObject
{
	[SerializeField] protected EntitySO entity;
	[SerializeField] protected DataSO data;

	protected GameObject baseObject;

	public virtual void InitialzeBase(GameObject baseObject)
	{
		this.baseObject = baseObject;
	}

	public virtual void InitializeParameters()
	{

	}
}
