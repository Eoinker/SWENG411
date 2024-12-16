using UnityEngine;
using UnityEngine.SceneManagement;

public class WinScreenManager : MonoBehaviour
{
    public void ReplayLevel()
    {
        // Reload the current level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
{
    string currentSceneName = SceneManager.GetActiveScene().name;
    string nextSceneName = $"Level_{int.Parse(currentSceneName.Replace("Level_", "")) + 1}";
    Debug.Log($"Loading next level: {nextSceneName}");
    if (Application.CanStreamedLevelBeLoaded(nextSceneName))
    {
        SceneManager.LoadScene(nextSceneName);
        Time.timeScale = 1f;
    }
    else
    {
        Debug.LogWarning("Next level does not exist.");
        LoadMainMenu();
        Time.timeScale = 1f;
    }
}


    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
