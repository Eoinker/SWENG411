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
        OnSimulationReset?.Invoke();
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
}
