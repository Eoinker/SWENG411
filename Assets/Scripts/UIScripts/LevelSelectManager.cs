using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void LoadLevel1()
    {
        Debug.Log("Loading Level 1...");
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevel2()
    {
        Debug.Log("Loading Level 2...");
        SceneManager.LoadScene("Level2");
    }
    public void LoadLevel3()
    {
        Debug.Log("Loading Level 3...");
        SceneManager.LoadScene("Level3");
    }
    public void LoadLevel4()
    {
        Debug.Log("Loading Level 4...");
        SceneManager.LoadScene("Level4");
    }
    public void LoadLevel5()
    {
        Debug.Log("Loading Level 5...");
        SceneManager.LoadScene("UITesting");
    }
    public void LoadLevel6()
    {
        Debug.Log("Loading Level 6...");
        SceneManager.LoadScene("UITesting");
    }


    public void LoadMainMenu()
    {
        Debug.Log("Returning to Main Menu...");
        SceneManager.LoadScene("MainMenu");
    }
}
