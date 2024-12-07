using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatCSV : ConditionalStatementValue
{
    private float value;

    public FloatCSV(float val)
    {
        value = val;
    }

    public override float GetValue(CharacterCommandManager ccm)
    {
        // CCM not needed here
        return value;
    } 
}
