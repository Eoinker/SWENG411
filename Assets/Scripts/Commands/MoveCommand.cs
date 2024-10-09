using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : CharacterCommand
{
    // Positive = right, Negative = left 
    int direction;

    public MoveCommand(CharacterControl cc, int direction)
    {
        this.controller = cc;

        if (direction > 0) {
            direction = 1;
        } else if (direction < 0)
        {
            direction = -1;
        } else {
            Debug.Log("MoveCommand Argument Cannot be Zero.");
        }
    }

    public override void Execute()
    {
        controller.SetMoveDirection(direction);
    }
}
