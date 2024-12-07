using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager : MonoBehaviour
{
    public CharacterControl controller;
    public Queue<CharacterCommand> commands;
    private Stack<CharacterCommand> runtimeStack;

    void Awake()
    {
        controller = GetComponent<CharacterControl>();
        commands = new Queue<CharacterCommand>();
        runtimeStack = new Stack<CharacterCommand>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        while (controller.IsExecuting() && (runtimeStack.Count > 0 || commands.Count > 0))
        {
            if (runtimeStack.Count > 0)
            {
                CharacterCommand command = runtimeStack.Pop();
                command.Execute(this);
            }
            else if (commands.Count > 0)
            {
                CharacterCommand command = commands.Dequeue();
                command.Execute(this);
            }
        }
    }

    public void PushToRuntimeStack(CharacterCommand command)
    {
        runtimeStack.Push(command);
    }
}
