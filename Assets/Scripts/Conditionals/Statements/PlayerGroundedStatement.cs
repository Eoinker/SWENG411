using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedStatement : ConditionalStatement
{
    private bool desiredState;

    public PlayerGroundedStatement(bool state)
    {
        desiredState = state;
    }

    public override bool GetStatementResult(CharacterCommandManager ccm)
    {
        if (ccm.controller.isGrounded == desiredState)
        {
            return true;
        } else {
            return false;
        }
    }
}