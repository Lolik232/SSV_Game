using System;
using UnityEngine;

public class PlayerAction
{
    private Boolean m_IsActive;

    public Boolean IsActive
    {
        get { return m_IsActive; }
        set { m_IsActive = value; }
    }

    public virtual void Start()
    {
        m_IsActive = true;
    }

    public virtual void End()
    {
        m_IsActive = false;
    }
}

