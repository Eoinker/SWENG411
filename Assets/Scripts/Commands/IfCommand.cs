using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IfCommand : CharacterCommand
{
    private Conditional condition;
    private Stack<CharacterCommand> commandStack;

    public IfCommand() {}
    public IfCommand(Conditional c, Stack<CharacterCommand> cs)
    {
        condition = c;
        commandStack = cs;
    }

    public override void Execute(CharacterCommandManager ccm)
    {
        if (condition.GetState() == true)
        {
            while (commandStack.Count > 0)
            {
                ccm.PushToRuntimeStack(commandStack.Pop());
            }
            
        }
    }
}
