using UnityEngine;


[CreateAssetMenu(fileName = "New Location", menuName = "Scene Data/Location")]
public class LocationSO : GameSceneSO
{
    public string locationName;

    public LocationSO()
    {
        sceneType = GameSceneType.Location;
    }
}
