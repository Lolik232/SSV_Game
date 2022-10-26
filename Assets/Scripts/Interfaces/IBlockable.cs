using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBlockable
{
	public void Block(bool needHardExit);

	public void Unlock();
}