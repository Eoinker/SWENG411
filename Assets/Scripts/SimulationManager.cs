using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

    private bool simIsRunning, isLevelComplete;
    private CharacterCommandManager[] commandManagers;
    private CodeParser[] parsers;
    private float simulationTime;

    public static Action OnSimulationReset, OnSimulationStart;

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
    }

    private void OnDisable()
    {
        CharacterControl.OnHazardHit -= ResetSimulation;
        SimulationUIManager.OnPlayButtonPress -= RunSimulation;
        SimulationUIManager.OnResetButtonPress -= ResetSimulation;
    }

    public void RunSimulation()
    {
        ResetSimulation();
        foreach (CodeParser parser in parsers)
        {
            parser.CompileCodeInput();
        }
        foreach (CharacterCommandManager ccm in commandManagers)
        {
            ccm.BeginExecution();
        }
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
            simulationTime += Time.deltaTime;
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

    public float GetTime() {
        // returns the current simulation time
        return simulationTime;
    }

    public bool IsLevelComplete()
    {
        return isLevelComplete;
    }
}
