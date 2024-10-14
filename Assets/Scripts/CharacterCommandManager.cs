using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCommandManager : MonoBehaviour
{
    private CharacterControl controller;
    private Queue<CharacterCommand> commands;

    void Awake()
    {
        controller = GetComponent<CharacterControl>();
        commands = new Queue<CharacterCommand>();

        commands.Enqueue(new MoveCommand(1));
        commands.Enqueue(new WaitForSecondsCommand(1f));
        commands.Enqueue(new MoveCommand(0));
        commands.Enqueue(new WaitForSecondsCommand(2f));
        commands.Enqueue(new MoveCommand(-1));
        commands.Enqueue(new WaitForSecondsCommand(0.5f));
        commands.Enqueue(new MoveCommand(1));
        commands.Enqueue(new WaitForSecondsCommand(0.5f));
        commands.Enqueue(new JumpCommand());
    }

    // Update is called once per frame
    void Update()
    {
        while (commands.Count > 0 && controller.IsExecuting())
        {
            Debug.Log("Executing Command");
            CharacterCommand command = commands.Dequeue();
            command.Execute(controller);
        }
    }
}
