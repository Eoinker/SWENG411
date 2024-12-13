using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationUIManager : MonoBehaviour
{
    public static Action OnPlayButtonPress, OnResetButtonPress, OnPauseButtonPress;

    public Button playButton, resetButton, pauseButton; // Set in Editor
    public TMP_InputField codeInputField;
    public GameObject errorPanel;
    public TMP_Text errorText;


    private void OnEnable()
    {
        SimulationManager.OnSimulationStart += ShowSimRunningButtons;
        SimulationManager.OnSimulationReset += ShowCodingButtons;
        SimulationManager.OnCompilationStart += DisableErrorText;


        CodeParser.OnLineError += UpdateErrorText;
    }

    private void OnDisable()
    {
        SimulationManager.OnSimulationStart -= ShowSimRunningButtons;
        SimulationManager.OnSimulationReset -= ShowCodingButtons;
        SimulationManager.OnCompilationStart -= DisableErrorText;

        CodeParser.OnLineError -= UpdateErrorText;
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

    public void PressPause()
    {
        OnPauseButtonPress?.Invoke();
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

    private void UpdateErrorText(string errorString)
    {
        errorText.text = errorString;
        errorPanel.gameObject.SetActive(true);
    }

    private void DisableErrorText()
    {
        errorPanel.gameObject.SetActive(false);
    }
}
