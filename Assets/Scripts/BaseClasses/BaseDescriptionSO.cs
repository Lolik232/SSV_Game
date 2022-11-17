using UnityEngine;

namespace All.BaseClasses
{
    /// <summary>
    /// Base class for all SO that need a description
    /// </summary>
    public class BaseDescriptionSO : SerializableScriptableObject
    {
        [TextArea] public string description;
    }
}