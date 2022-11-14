using System;
using All.Events;
using Systems.SaveSystem.Settings.ScriptableObjects;
using UnityEngine;

namespace Systems.SaveSystem
{
    [CreateAssetMenu(fileName = "Save System", menuName = "Save/System")]
    public class SaveSystem : ScriptableObject
    {
        [SerializeField] private VoidEventChannelSO _saveSettingsEvent = default;
        [SerializeField] private LoadEventChannelSO _saveGameEvent     = default;
        [SerializeField] private SettingsSO         _currentSettings   = default;

        [SerializeField] private string _saveFileName     = "LIND.save";
        [SerializeField] private string _settingsFilename = "settings.save";

        public string SaveFileName => _saveFileName;

        public Save save = new Save();

        private void OnEnable()
        {
            _saveSettingsEvent.OnEventRaised += SaveSettingsOnDisk;
            _saveGameEvent.OnEventRaised     += OnSaveGame;
        }

        private void OnSaveGame(GameSceneSO loadedLocation, bool showLoadingScreen, bool fadeScreen)
        {
            LocationSO location = loadedLocation as LocationSO;
            if (location)
            {
                save.locationID = location.Guid;
            }

            SaveGameOnDisk();
        }

        public bool HasSave()
        {
            return FileManager.IsExist(_saveFileName);
        }

        public void SaveGameOnDisk()
        {
            var saveJson = save.ToJson();
            FileManager.WriteToFile(_saveFileName, "", saveJson);
        }

        public bool LoadSettingsFromDisk()
        {
            string settingsJson;
            var    loaded = FileManager.LoadFromFile(_settingsFilename, out settingsJson);
            if (loaded == false) return false;
            _currentSettings.FromJson(settingsJson);
            return true;
        }

        public void SaveSettingsOnDisk()
        {
            var settingsJson = _currentSettings.ToJson();
            FileManager.WriteToFile(_settingsFilename, "", settingsJson);
        }

        public void ClearSettings()
        {
            FileManager.RemoveFile(_settingsFilename);
        }
    }
}