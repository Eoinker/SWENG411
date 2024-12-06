using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerPositonConditional : Conditional
{
    private bool isHorizontal;
    private int comparisonType;
    private float positionValue;

    public PlayerPositonConditional(bool horizontal, int comp, float val)
    {
        
    }

    public override bool GetValue(CharacterCommandManager ccm)
    {

    }
}
