using System;
using UnityEngine;

namespace All.Events
{
    [CreateAssetMenu(menuName = "Events/Void Event Channel")]
    public class VoidEventChannelSO : EventChannelSO
    {
        private void OnEnable()
        {
            Debug.Log("EVENT");
        }
    }
}