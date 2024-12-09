using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : CharacterCommand
{
    private ConditionalStatement condition;
    private Stack<CharacterCommand> commandStack;

    // TEMP
    public IfCommand() {}
    // TEMP

    public IfCommand(ConditionalStatement cond, Stack<CharacterCommand> comStack)
    {
        condition = cond;
        commandStack = comStack;
    }

    public override void Execute(CharacterCommandManager ccm)
    {
        if (condition.GetStatementResult(ccm) == true)
        {
            Stack<CharacterCommand> tempStack = new Stack<CharacterCommand>(commandStack);
            while (tempStack.Count > 0)
            {
                ccm.PushToRuntimeStack(tempStack.Pop());
            }
            
        }
    }
}
