using UnityEngine;

namespace Systems.SaveSystem.Settings.ScriptableObjects
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
    public class SettingsSO : ScriptableObject
    {
        public float masterVolume    = default;
        public float musicVolume   = default;
        public float effectsVolume = default;
        
        public SettingsSO(){}
        
        public void LoadSavedSettings(SettingsSave settingsSave)
        {
            masterVolume  = settingsSave.masterVolume;
            musicVolume   = settingsSave.musicVolume;
            effectsVolume = settingsSave.effectsVolume;
        }
    }
}