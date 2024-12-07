using UnityEngine;
using UnityEngine.SceneManagement; // For scene loading

public class MainMenuHandler : MonoBehaviour
{
    // Called when the "Play" button is clicked
    public void LoadLevelSelect()
    {
        Debug.Log("Loading Level Select...");
        SceneManager.LoadScene("LevelSelect"); // Replace "Gameplay" with the name of your game scene
    }

    // Called when the "Quit" button is clicked
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit(); // Works in builds, not the editor
    }
}
