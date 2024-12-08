using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForCommand : CharacterCommand
{
    private int loopCount;
    private Stack<CharacterCommand> commandStack;

    public ForCommand(int count, Stack<CharacterCommand> comStack)
    {
        loopCount = count;
        commandStack = comStack;
    }

    public override void Execute(CharacterCommandManager ccm)
    {
        if (loopCount > 0)
        {
            // Adding new for command with 1 less count to the bottom of the runtime stack
            ccm.PushToRuntimeStack(new ForCommand(loopCount - 1, commandStack));

            Stack<CharacterCommand> tempStack = new Stack<CharacterCommand>(commandStack);
            while (tempStack.Count > 0)
            {
                ccm.PushToRuntimeStack(tempStack.Pop());
            }
        }
    }
}
