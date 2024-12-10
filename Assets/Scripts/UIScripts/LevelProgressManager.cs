using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //saves the current level progress as compelted or incomplete
    public static void SetLevelComplete(int level)
    {
        PlayerPrefs.SetInt("Level" + level, 1);
        PlayerPrefs.Save();
    }
    //check if level is complete
    public static bool isLevelComplete(int level)
    {
        return PlayerPrefs.GetInt("Level" + level, 0) == 1;
    }

    //Unlock the next level
    public static void Unlocklevel(int level)
    {
        PlayerPrefs.SetInt("Level" + level, 1);
        PlayerPrefs.Save();
    }
}
