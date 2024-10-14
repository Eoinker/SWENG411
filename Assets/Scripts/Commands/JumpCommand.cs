using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCommand : CharacterCommand
{
    public JumpCommand()
    {

    }

    public override void Execute(CharacterControl controller)
    {
        controller.TryJump();
    }
}
