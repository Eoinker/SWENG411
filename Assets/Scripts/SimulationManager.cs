using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;
    private float simulationTime = 0f; // Tracks the simulation time
    private bool simIsRunning;
    private CharacterCommandManager[] commandManagers;
    private CodeParser[] parsers;

    public static Action OnSimulationReset, OnSimulationStart, OnCompilationStart;
    public bool isLevelComplete;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.Log("Duplicate simulation manager instance");
            Destroy(this);
        } else {
            Instance = this;

            // Do normal Awake functionality
            commandManagers = FindObjectsByType<CharacterCommandManager>(FindObjectsSortMode.None);
            parsers = FindObjectsByType<CodeParser>(FindObjectsSortMode.None);
        }
    }

    private void OnEnable()
    {
        CharacterControl.OnHazardHit += ResetSimulation;
        SimulationUIManager.OnPlayButtonPress += RunSimulation;
        SimulationUIManager.OnResetButtonPress += ResetSimulation;
        SimulationUIManager.OnPauseButtonPress += PauseSimulation;
        SimulationUIManager.OnResumeButtonPress += ResumeSimulation;
    }

    private void OnDisable()
    {
        CharacterControl.OnHazardHit -= ResetSimulation;
        SimulationUIManager.OnPlayButtonPress -= RunSimulation;
        SimulationUIManager.OnResetButtonPress -= ResetSimulation;
        SimulationUIManager.OnPauseButtonPress -= PauseSimulation;
        SimulationUIManager.OnResumeButtonPress = ResumeSimulation;
    }

    public void RunSimulation()
    {
        ResetSimulation();

        OnCompilationStart?.Invoke();
        bool isError = false;
        foreach (CodeParser parser in parsers)
        {
            if (parser.CompileCodeInput() == false)
            {
                isError = true;
            }
        }
        if (isError == true)
        {
            return;
        }

        foreach (CharacterCommandManager ccm in commandManagers)
        {
            ccm.BeginExecution();
        }
        simulationTime = 0f; // Reset the simulation time
        simIsRunning = true;
        OnSimulationStart?.Invoke();
    }

    public void ResetSimulation()
    {
        foreach (CharacterCommandManager ccm in commandManagers)
        {
            ccm.Reset();
        }
        simIsRunning = false;
        simulationTime = 0;
        isLevelComplete = false;
        OnSimulationReset?.Invoke();
    }

    public void PauseSimulation()
    {
        Time.timeScale = 0f;
    }

    public void ResumeSimulation()
    {
        Time.timeScale = 1f;
    }


    public void FixedUpdate()
    {
        if (simIsRunning)
        {
            simulationTime += Time.fixedDeltaTime; // Increment time during simulation
            foreach (CharacterCommandManager ccm in commandManagers)
            {
                ccm.ExecuteNextCommand();
            }

            // Debug.Log(string.Format("{0:0.00}", GetTime()));
        }   
    }


    public bool IsRunning()
    {
        return simIsRunning;
    }

    public float GetTime()
    {
        return simulationTime; // Return the current simulation time
    }
    public int GetLines()
    {
        int lines = 0;
        foreach (CodeParser parser in parsers)
        {
            lines += parser.linesOfCode;
        }
        return lines;
    }
}
