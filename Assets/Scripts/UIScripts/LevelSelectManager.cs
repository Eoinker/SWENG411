using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelectManager : MonoBehaviour
{
    //public Button[] levelButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void UpdateLevelButtons()
    {
       /* for( int i = 0; i <  levelButtons.Length; i++)
        {
            //enables a button if previous level is completed
            if (i == 0 || LevelProgressManager.IsLevelComplete(i))
            {
                levelButtons[i].interactable = true;
            }
            else
                levelButtons[i].interactable = false;
        }*/
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
