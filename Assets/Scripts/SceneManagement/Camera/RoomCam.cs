using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(Collider2D))]
public class RoomCam : MonoBehaviour
{
    private CinemachineVirtualCamera m_currentRoomCam = default;

    private void Awake()
    {
        m_currentRoomCam         = GetComponent<CinemachineVirtualCamera>();
        m_currentRoomCam.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<EntityBase>() == null)
        {
            return;
        }

        m_currentRoomCam.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<EntityBase>() == null)
        {
            return;
        }

        m_currentRoomCam.enabled = false;
    }
}