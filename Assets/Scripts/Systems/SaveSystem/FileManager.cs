using System;
using System.IO;
using UnityEngine;
using Application = UnityEngine.Application;

namespace Systems.SaveSystem
{
    public static class FileManager
    {
        public static bool WriteToFile(string fileName, string directory, string json)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, directory, fileName);

            try
            {
                File.WriteAllText(fullPath, json);
                return true;
            } catch (Exception e)
            {
                Debug.LogError($"Filed to write file on disk. Path: {fullPath} exception {e}");
                return false;
            }
        }
        
        public static bool LoadFromFile(string path, out string json)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            if (!File.Exists(fullPath))
            {
                json = "";
                return false;
            }

            try
            {
                json = File.ReadAllText(fullPath);
                return true;
            } catch (Exception e)
            {
                Debug.LogError($"Failed read file from disk. Path: {fullPath} exception {e}");
                json = "";
                return false;
            }
        }

        public static bool LoadFromFile(string fileName, string directory, out string json)
        {
            var path = Path.Combine(directory, fileName);
            return LoadFromFile(path, out json);
        }

        public static bool IsExist(string path)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            return File.Exists(fullPath);
        }

        public static bool IsExist(string fileName, string directory)
        {
            var path = Path.Combine(directory, fileName);
            return IsExist(path);
        }

        public static bool RemoveFile(string path)
        {
            var fullPath = Path.Combine(Application.persistentDataPath, path);
            try
            {
                File.Delete(fullPath);
                return true;
            } catch (Exception e)
            {
                Debug.LogError($"Failed remove file from disk. Path: {fullPath} exception {e}");
                return false;
            }
        }

        public static bool RemoveFile(string fileName, string directory)
        {
            var path = Path.Combine(directory, fileName);
            return RemoveFile(path);
        }
    }
}