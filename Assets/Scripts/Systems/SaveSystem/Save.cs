using System;
using UnityEngine;

namespace Systems.SaveSystem
{
    [Serializable]
    public class Save
    {
        public string locationID;
        //TODO: add location rooms state and roomID, where player stop playing

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