using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    // Start is called before the first frame update
  

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void nextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        
        // Check if the next scene index is valid
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.Log("No more levels. Returning to main menu.");
            SceneManager.LoadScene("MainMenu"); // Replace with your main menu scene
            Time.timeScale = 1f;
        }
    }
    
}
