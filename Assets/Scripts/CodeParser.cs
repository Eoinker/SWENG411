using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeParser : MonoBehaviour
{
    public List<string> script;
    public CharacterCommandManager ccm;
    public int currentLine;

    public void Start()
    {
        ccm.commands = ConvertScriptToCommandQueue();
    }

    public Queue<CharacterCommand> ConvertScriptToCommandQueue()
    {
        Queue<CharacterCommand> commands = new Queue<CharacterCommand>();
        
        while (currentLine < script.Count)
        {
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(script[currentLine]);
            } catch (InvalidLineException e)
            {
                Debug.LogException(e);
                continue;
            }

            commands.Enqueue(lineCommand);
            currentLine++;
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

        // force all letters to lowercase

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
            case "wait":
                try {
                    return ParseWaitCommand(split);
                } catch (InvalidLineException e) {
                    throw new InvalidLineException();
                }
            case "jump":
                return new JumpCommand();
                // try {
                //     return ParseJumpCommand();
                // } catch (InvalidLineException e) {
                //     throw new InvalidLineException();
                // }
            case "if":
                return ParseIfCommand(split);
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
            case "r":
            case "right":
                return new MoveCommand(1);
            case "l":
            case "left":
                return new MoveCommand(-1);
            case "s":
            case "stop":
                return new MoveCommand(0);
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

        // attempt to convert second string into a float value
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
        return new JumpCommand();
    }

    private CharacterCommand ParseIfCommand(string[] slitLine)
    {
        // Checking what the second word of the command is
        return new IfCommand();
        
    }



    private Conditional ParseConditional(string[] strings)
    {
        switch (strings[1])
        {
            case "grounded":
                return new PlayerGroundedConditional(true);
            case "!grounded":
                return new PlayerGroundedConditional(false);
            case "x":
            case "horizontal":
                return new PlayerXConditional();
            case "y":
            case "vertical":
                return new PlayerYConditional();
            case "time":
                return new SimulationTimeConditional(0);
            default:
                throw new InvalidLineException();
        }
    }
}
