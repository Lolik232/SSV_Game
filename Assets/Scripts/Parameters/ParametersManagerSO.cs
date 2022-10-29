using UnityEngine;

public abstract class ParametersManagerSO : StaticManagerSO<Parameter>
{
	//public Parameter moveUpSpeed;
	//public Parameter moveDownSpeed;
	//public Parameter moveForwardSpeed;
	//public Parameter moveBackwardSpeed;

	protected override void OnEnable()
	{
		base.OnEnable();

		//elements.Add(moveUpSpeed);
		//elements.Add(moveDownSpeed);
		//elements.Add(moveForwardSpeed);
		//elements.Add(moveBackwardSpeed);
	}

	public override void Initialize(GameObject origin)
	{
		base.Initialize(origin);
		foreach (var element in elements)
		{
			element.Set(element.Max);
		}
	}
}
