using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoSingleton<LevelLoader>
{
    public void LoadLevel(int buildIndex)
    {
        // Safety check
        if (buildIndex < 0 || buildIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogError("LevelLoader: Invalid build index " + buildIndex);
            return;
        }

        SceneManager.LoadScene(buildIndex);
    }

    public void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
    }
}
