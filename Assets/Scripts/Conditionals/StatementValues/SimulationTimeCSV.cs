using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationTimeCSV : ConditionalStatementValue
{
    public override float GetValue(CharacterCommandManager ccm)
    {
        // CCM not needed here
        return SimulationManager.Instance.GetTime();
    }
}