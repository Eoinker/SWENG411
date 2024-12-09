using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    public static SimulationManager Instance;

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

    public float GetTime() {
        // returns the current simulation time
        return 0;
    }
}
