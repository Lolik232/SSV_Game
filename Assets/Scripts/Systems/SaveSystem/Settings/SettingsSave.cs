using System;
using Systems.SaveSystem.Settings.ScriptableObjects;
using UnityEngine;

namespace Systems.SaveSystem.Settings
{
    [Serializable]
    public class SettingsSave
    {
        public float masterVolume  = default;
        public float musicVolume   = default;
        public float effectsVolume = default;

        public void SaveSettings(SettingsSO currentSettings)
        {
            masterVolume  = currentSettings.masterVolume;
            musicVolume   = currentSettings.musicVolume;
            effectsVolume = currentSettings.effectsVolume;
        }
        
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