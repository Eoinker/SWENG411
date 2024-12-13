using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletedStatement : ConditionalStatement
{
    private bool desiredState;

    public LevelCompletedStatement(bool state)
    {
        desiredState = state;
    } 

    public override bool GetStatementResult(CharacterCommandManager ccm)
    {
        // CCM not needed here
        if (SimulationManager.Instance.isLevelComplete == desiredState)
        {
            return true;
        } else {
            return false;
        }
    }
}
