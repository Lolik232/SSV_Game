using UnityEngine;

namespace All.Events
{
    [CreateAssetMenu(fileName = "StateChangeEventChannel", menuName = "Events/State Change Event Channel")]

    public class StateChangeEventChannelSO : TypedEventChannelSO<StateSO>
    {
    }
}
