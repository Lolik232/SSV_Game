using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyState : State
{
    protected Dummy Dummy
    {
        get;
        private set;
    }

    protected override void Awake()
    {
        base.Awake();
        Dummy = GetComponent<Dummy>();
    }
}
