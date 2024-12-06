using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerGroundedConditional : Conditional
{
    private bool desiredState;

    public PlayerGroundedConditional(bool state)
    {
        desiredState = state;
    }

    public override bool GetValue(CharacterCommandManager ccm)
    {
        if (ccm.controller.isGrounded == desiredState)
        {
            return true;
        } else {
            return false;
        }
    }
}