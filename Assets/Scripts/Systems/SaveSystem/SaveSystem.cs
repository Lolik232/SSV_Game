using System;
using All.Events;
using Systems.SaveSystem.Settings;
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

        public Save         save          = new Save();
        public SettingsSave SavedSettings = new SettingsSave();

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

        public void SetupSave(GameSceneSO lastLocation)
        {
            save.locationID = lastLocation.Guid;
        }

        public void SaveGameOnDisk()
        {
            var saveJson = save.ToJson();
            FileManager.WriteToFile(_saveFileName, "", saveJson);
        }

        public void RemoveGameFromDisk()
        {
            FileManager.RemoveFile(_saveFileName);
        }

        public bool LoadSettingsFromDisk()
        {
            string settingsJson;
            var    loaded = FileManager.LoadFromFile(_settingsFilename, out settingsJson);
            if (!loaded) return false;

            SavedSettings.FromJson(settingsJson);
            return true;
        }

        public bool LoadDataFromDisk()
        {
            string json;
            FileManager.LoadFromFile(_saveFileName, out json);
            save.FromJson(json);

            return true;
        }
        
        public void SaveSettingsOnDisk()
        {
            SavedSettings.SaveSettings(_currentSettings);
            var settingsJson = SavedSettings.ToJson();
            FileManager.WriteToFile(_settingsFilename, "", settingsJson);
        }
        public void ClearSettings()
        {
            FileManager.RemoveFile(_settingsFilename);
        }
    }
}