using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimulationUIManager : MonoBehaviour
{
    // events for when each button is pressed
    public static Action OnPlayButtonPress, OnResetButtonPress, OnPauseButtonPress, OnResumeButtonPress;

    // Set in Editor
    public Button playButton, resetButton, pauseButton, resumeButton; 
    public TMP_InputField codeInputField;
    public GameObject errorPanel;
    public TMP_Text errorText;


    private void OnEnable()
    {
        // subscribe to other events
        SimulationManager.OnSimulationStart += ShowSimRunningButtons;
        SimulationManager.OnSimulationReset += ShowCodingButtons;
        SimulationManager.OnCompilationStart += DisableErrorText;


        CodeParser.OnLineError += UpdateErrorText;
    }

    private void OnDisable()
    {
        // unsubscribe
        SimulationManager.OnSimulationStart -= ShowSimRunningButtons;
        SimulationManager.OnSimulationReset -= ShowCodingButtons;
        SimulationManager.OnCompilationStart -= DisableErrorText;

        CodeParser.OnLineError -= UpdateErrorText;
    }


    public void PressPlay()
    {
        // update visible buttons
        ShowSimRunningButtons();

        // trigger event
        OnPlayButtonPress?.Invoke();
    }

    public void PressReset()
    {
        // update visible buttons
        ShowCodingButtons();

        // trigger event
        OnResetButtonPress?.Invoke();
    }

    public void PressPause()
    {
        // trigger event
        OnPauseButtonPress?.Invoke();
        // hide pause button
        pauseButton.gameObject.SetActive(false);
        // show resume button
        resumeButton.gameObject.SetActive(true);
        
    }

    public void PressResume()
    {
        // trigger event
        OnResumeButtonPress?.Invoke();
        // hide resume button
        resumeButton.gameObject.SetActive(false);
        // show pause button
        pauseButton.gameObject.SetActive(true);
    }

    private void ShowSimRunningButtons()
    {
        // show pause and reset buttons
        resetButton.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        // hide resume and play buttons
        resumeButton.gameObject.SetActive(false);
        playButton.gameObject.SetActive(false);
    }

    private void ShowCodingButtons()
    {
        // hide pause, reset, and resume buttons
        resetButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        // show play button
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
