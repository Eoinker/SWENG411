using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager : MonoBehaviour
{
    private CharacterControl controller;
    public Queue<CharacterCommand> commands;

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
        while (commands.Count > 0 && controller.IsExecuting())
        {
            CharacterCommand command = commands.Dequeue();
            command.Execute(controller);
        }
    }
}
