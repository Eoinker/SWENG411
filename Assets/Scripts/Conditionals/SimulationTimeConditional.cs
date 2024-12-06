using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationTimeConditional : Conditional
{
    private int comparisonType;
    private float timeValue;

    public SimulationTimeConditional(int comp, float val)
    {
        comparisonType = comp;
        timeValue = val;
    }

    public override bool GetValue()
    {
        float currentTime = SimulationManager.Instance.GetTime();
        return CompareNumbers(currentTime, timeValue, comparisonType);
    }
}
