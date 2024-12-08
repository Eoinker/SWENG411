using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionCSV : ConditionalStatementValue
{
    private bool isHorizontal;

    public PlayerPositionCSV(bool horizontal)
    {
        isHorizontal = horizontal;
    }

    public override float GetValue(CharacterCommandManager ccm)
    {
        if (isHorizontal)
        {
            return ccm.transform.position.x;
        } else {
            return ccm.transform.position.y;
        }
    }
}