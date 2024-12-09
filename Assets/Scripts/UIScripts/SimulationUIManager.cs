using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationUIManager : MonoBehaviour
{
    public static Action OnPlayButtonPress, OnResetButtonPress;

    public Button playButton, resetButton, pauseButton; // Set in Editor
    public TMP_InputField codeInputField;

    public void Update()
    {

    }

    private void OnEnable()
    {
        SimulationManager.OnSimulationStart += ShowSimRunningButtons;
        SimulationManager.OnSimulationReset += ShowCodingButtons;
    }

    private void OnDisable()
    {
        SimulationManager.OnSimulationStart -= ShowSimRunningButtons;
        SimulationManager.OnSimulationReset -= ShowCodingButtons;
    }


    public void PressPlay()
    {
        ShowSimRunningButtons();

        OnPlayButtonPress?.Invoke();
    }

    public void PressReset()
    {
        ShowCodingButtons();

        OnResetButtonPress?.Invoke();
    }

    private void ShowSimRunningButtons()
    {
        resetButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        playButton.gameObject.SetActive(false);
    }

    private void ShowCodingButtons()
    {
        resetButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(true);
    }
}
