using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeParser : MonoBehaviour
{
    public List<string> script;
    public CharacterCommandManager ccm;

    public void Start()
    {
        ccm.commands = ConvertScriptToCommandQueue();
    }

    public Queue<CharacterCommand> ConvertScriptToCommandQueue()
    {
        Queue<CharacterCommand> commands = new Queue<CharacterCommand>();

        foreach (string line in script)
        {
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(line);
            } catch (InvalidLineException e)
            {
                Debug.LogException(e);
                continue;
            }

            commands.Enqueue(lineCommand);
        }

        return commands;
    }

    public CharacterCommand ConvertLineToCommand(string line)
    {
        // Check if line is empty
        if (string.IsNullOrEmpty(line))
        {
            throw new InvalidLineException();
        }

        string[] split = line.Split(' ');
        string firstWord = split[0];

        switch (firstWord)
        {
            case "move":
                try {
                    return ParseMoveCommand(split);
                } catch (InvalidLineException e) {
                    throw new InvalidLineException();
                }
                break;
            case "wait":
                try {
                    return ParseWaitCommand(split);
                } catch (InvalidLineException e) {
                    throw new InvalidLineException();
                }
                break;
            case "jump":
                return new JumpCommand();
                // try {
                //     return ParseJumpCommand();
                // } catch (InvalidLineException e) {
                //     throw new InvalidLineException();
                // }
                break;
            default:
                throw new InvalidLineException();
        }
    }

    private CharacterCommand ParseMoveCommand(string[] splitLine)
    {
        // Move command only takes one additional word after
        if (splitLine.Length != 2)
        {
            throw new InvalidLineException();
        }

        // Checking what the second word of the command is
        switch (splitLine[1])
        {
            case "right":
                return new MoveCommand(1);
                break;
            case "left":
                return new MoveCommand(-1);
                break;
            case "stop":
                return new MoveCommand(0);
                break;
            default:
                throw new InvalidLineException();
        }
    }

    private CharacterCommand ParseWaitCommand(string[] splitLine)
    {
        // if the line is just "wait" with nothing after 
        if (splitLine.Length == 1)
        {
            return new WaitForFramesCommand(1);
        }

        // attempt to convert second word into a float value
        float seconds = 0;
        try {
            seconds = float.Parse(splitLine[1]);
        } catch (Exception e)
        {
            throw new InvalidLineException();
        }

        return new WaitForSecondsCommand(seconds);
    }

    private CharacterCommand ParseJumpCommand(string[] splitLine)
    {
        // do this later
        return new JumpCommand();
    }
}
