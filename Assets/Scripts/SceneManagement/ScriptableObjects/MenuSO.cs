using UnityEngine;


[CreateAssetMenu(fileName = "New Location", menuName = "Scene Data/Menu", order = 0)]
public class MenuSO : GameSceneSO
{
	public MenuSO()
	{
		sceneType = GameSceneType.Menu;
	}
}