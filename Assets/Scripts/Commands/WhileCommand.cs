using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhileCommand : CharacterCommand
{
    private ConditionalStatement condition;
    private Stack<CharacterCommand> commandStack;

    public WhileCommand(ConditionalStatement cond, Stack<CharacterCommand> comStack)
    {
        condition = cond;
        commandStack = comStack;
    }

    public override void Execute(CharacterCommandManager ccm)
    {
        if (condition.GetStatementResult(ccm) == true)
        {
            // Adding duplicate while command to the bottom of the runtime stack
            ccm.PushToRuntimeStack(this);

            Stack<CharacterCommand> tempStack = new Stack<CharacterCommand>(commandStack);
            while (tempStack.Count > 0)
            {
                ccm.PushToRuntimeStack(tempStack.Pop());
            }
        }
    }
}
