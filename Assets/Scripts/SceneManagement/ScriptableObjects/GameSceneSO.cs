using UnityEngine;
using UnityEngine.AddressableAssets;

public class GameSceneSO : ScriptableObject
{
    public GameSceneType sceneType;
    public AssetReference sceneReference;


    public enum GameSceneType
    {
        Location,
        Menu,

        Managers,
    }
}