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
            Debug.Log("IN CONDITION Player is " + (ccm.controller.isGrounded ? "grounded" : "not grounded"));
            Debug.Log("IN CONDITION desired state is " + (desiredState));
            return true;
        } else {
            return false;
        }
    }
}