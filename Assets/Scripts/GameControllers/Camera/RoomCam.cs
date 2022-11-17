using Cinemachine;

using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
[RequireComponent(typeof(Collider2D))]
public class RoomCam : MonoBehaviour
{
    private CinemachineVirtualCamera _currentRoomCam = default;

    private void Awake()
    {
        _currentRoomCam = GetComponent<CinemachineVirtualCamera>();
        _currentRoomCam.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<Player>() == null)
        {
            return;
        }

        _currentRoomCam.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.GetComponent<Player>() == null)
        {
            return;
        }

        _currentRoomCam.enabled = false;
    }
}