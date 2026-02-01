using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
   public void OnLoadLevelClicked(int index)
    {
        LevelLoader.Instance.LoadLevel(index);
    }

    public void QuiteGame()
    {
        Application.Quit();
    }
}
