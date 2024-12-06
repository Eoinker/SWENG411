using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : CharacterCommand
{
    // Positive = right, Negative = left 
    int moveDirection;

    public MoveCommand(int direction)
    {
        if (direction > 0) {
            // Moving Right
            moveDirection = 1;
        } else if (direction < 0)
        {
            // Moving Left
            moveDirection = -1;
        } else {
            // Stoping Movement
            moveDirection = 0;
        }
    }

    public override void Execute(CharacterCommandManager ccm)
    {
        ccm.controller.SetMoveDirection(moveDirection);
    }
}
