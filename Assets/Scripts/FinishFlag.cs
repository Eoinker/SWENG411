using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishFlag : MonoBehaviour
{
    public GameObject WinScreen;
    public bool isFinished = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {

            showWinScreen();
        }
    }
    public void showWinScreen()
    {
        isFinished = true;
        WinScreen.SetActive(isFinished);
        Time.timeScale = 0f;
    }
}
