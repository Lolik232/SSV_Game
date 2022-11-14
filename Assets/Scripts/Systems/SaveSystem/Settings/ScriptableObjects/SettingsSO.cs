using UnityEngine;

namespace Systems.SaveSystem.Settings.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class SettingsSO : ScriptableObject
    {
        public float gameVolume    = default;
        public float musicVolume   = default;
        public float effectsVolume = default;

        
        public string ToJson()
        {
            return JsonUtility.ToJson(this);
        }

        public void FromJson(string json)
        {
            JsonUtility.FromJsonOverwrite(json, this);
        }
    }
}