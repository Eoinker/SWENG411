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
    }

    // void Start()
    // {
    //     InvokeRepeating("Reset", 0f, 5f);
    // }

    // private void Reset()
    // {
    //     commands.Enqueue(new MoveCommand(1));
    //     commands.Enqueue(new WaitForSecondsCommand(1f));
    //     commands.Enqueue(new MoveCommand(0));
    //     commands.Enqueue(new WaitForSecondsCommand(2f));
    //     commands.Enqueue(new MoveCommand(-1));
    //     commands.Enqueue(new WaitForSecondsCommand(0.5f));
    //     commands.Enqueue(new MoveCommand(1));
    //     commands.Enqueue(new WaitForSecondsCommand(0.5f));
    //     commands.Enqueue(new JumpCommand());

    //     this.transform.position = new Vector2(-10, -1);
    // }

    void Update()
    {
        while (controller.IsExecuting())
        {
            if (runtimeStack.Count > 0)
            {
                CharacterCommand command = runtimeStack.Pop();
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
