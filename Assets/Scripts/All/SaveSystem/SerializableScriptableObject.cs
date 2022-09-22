using UnityEngine;
using UnityEngine.Serialization;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SerializableScriptableObject : ScriptableObject
{
    [FormerlySerializedAs("_guid")] [SerializeField, HideInInspector] private string m_guid;
    public string Guid => m_guid;

#if UNITY_EDITOR
    void OnValidate()
    {
        var path = AssetDatabase.GetAssetPath(this);
        m_guid = AssetDatabase.AssetPathToGUID(path);
    }
#endif
}
