using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro

public class FinishFlag : MonoBehaviour
{
    public GameObject WinScreen;         // Reference to the finish screen
    public TMP_Text TimeText;            // Text for displaying time
    public TMP_Text LinesText;           // Text for displaying lines of code
    public bool isFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowWinScreen();
        }
    }
    
    public void ShowWinScreen()
    {
        isFinished = true;
        WinScreen.SetActive(isFinished);

        // Update the time and lines text
        if (SimulationManager.Instance != null)
        {
            float time = SimulationManager.Instance.GetTime();
            TimeText.text = $"Time: {time:F2} seconds";
        }
        if (SimulationManager.Instance != null)
        {
            int lines = SimulationManager.Instance.GetLines();
            LinesText.text = $"Lines of Code: {lines}";
        }
        // Pause the game
        Time.timeScale = 0f;
    }
}
