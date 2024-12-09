using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeParser : MonoBehaviour
{
    public List<string> script;
    public CharacterCommandManager ccm;
    public int currentLine;
    public TMP_InputField codeInputField;

    public void CompileCodeInput() {
        SetScript(codeInputField.text);
        ccm.StopExecuting();
        ccm.SetCommandQueue(ConvertScriptToCommandQueue());
    }

    public void RunCodeInput()
    {
        CompileCodeInput();
        Debug.Log("Compiling Complete. Starting Execution!");
        ccm.BeginExecution();
    }

    private void SetScript(string rawCode) {
        List<string> rawSplit = new List<string>(rawCode.Split('\n'));

        foreach (string line in rawSplit)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                // Ignore any lines that are empty 
                continue;
            } else  {
                string trimmedLine = line.Trim();
                if (trimmedLine[0] == '/') {
                    // Ignore any comment lines (lines that start with /) 
                    continue;
                } else {
                    script.Add(trimmedLine);
                }
            }
        }
    }

    private Queue<CharacterCommand> ConvertScriptToCommandQueue()
    {
        Queue<CharacterCommand> commands = new Queue<CharacterCommand>();
        currentLine = 0;

        while (currentLine < script.Count)
        {
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(script[currentLine]);
            } catch (InvalidLineException e)
            {
                Debug.LogException(e);
                Debug.Break();
                return new Queue<CharacterCommand>();
                // return whatever commands have been converted so far
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

        switch (split[0])
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
                Debug.Log("Jump Command");
                return new JumpCommand();
                // try {
                //     return ParseJumpCommand();
                // } catch (InvalidLineException e) {
                //     throw new InvalidLineException();
                // }
            case "if":
                return ParseIfCommand(split);
            case "while":
                return ParseWhileCommand(split);
            case "for":
                return ParseForCommand(split);
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

    private CharacterCommand ParseIfCommand(string[] splitLine)
    {
        // Checking if there is anything after if
        if (splitLine.Length == 1)
        {
            // There cant be nothing after the if
            throw new InvalidLineException();
        }

        ConditionalStatement cond;
        try {
            cond = ParseConditionalStatement(splitLine.Skip(1).ToArray());
        } catch (Exception e) {
            throw new InvalidLineException();
        }
        
        // Stack of commands to run if the if statement is true
        Stack<CharacterCommand> comStack = new Stack<CharacterCommand>();
        bool foundEnd = false;
        currentLine++; // THIS MOVES TO THE FIRST LINE OF THE IF BLOCK
        // without this, it causes a stack overflow lmao

        while (currentLine < script.Count)
        {
            // Stop when the line reads "end"
            if (script[currentLine] == "end")
            {
                foundEnd = true;
                break;
            }

            // Otherwise, parse this line and add it to the comStack
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(script[currentLine]);
            } catch (InvalidLineException e)
            {
                throw new InvalidLineException();
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new IfCommand(cond, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException();
        }
    }

    private CharacterCommand ParseWhileCommand(string[] splitLine)
    {
        // Checking if there is anything after while
        if (splitLine.Length == 1)
        {
            // There cant be nothing after the while
            throw new InvalidLineException();
        }

        ConditionalStatement cond;
        try {
            cond = ParseConditionalStatement(splitLine.Skip(1).ToArray());
        } catch (Exception e) {
            throw new InvalidLineException();
        }
        
        // Stack of commands to run if the while statement is true
        Stack<CharacterCommand> comStack = new Stack<CharacterCommand>();
        bool foundEnd = false;
        currentLine++; // without this, it causes a stack overflow again

        while (currentLine < script.Count)
        {
            // Stop when the line reads "end"
            if (script[currentLine] == "end")
            {
                foundEnd = true;
                break;
            }

            // Otherwise, parse this line and add it to the comStack
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(script[currentLine]);
            } catch (InvalidLineException e)
            {
                throw new InvalidLineException();
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new WhileCommand(cond, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException();
        }
    }

    private CharacterCommand ParseForCommand(string[] splitLine)
    {
        if (splitLine.Length != 2)
        {
            // For loops must have exactly thing after them
            throw new InvalidLineException();
        }

        // Parsing the number of iterations to run
        int count = 0;
        try {
            count = int.Parse(splitLine[1]);
        } catch (Exception e)
        {
            throw new InvalidLineException();
        }
        
        // Stack of commands to run "count" number of times
        Stack<CharacterCommand> comStack = new Stack<CharacterCommand>();
        bool foundEnd = false;
        currentLine++; // again, no this = stack overflow

        while (currentLine < script.Count)
        {
            // Stop when the line reads "end"
            if (script[currentLine] == "end")
            {
                foundEnd = true;
                break;
            }

            // Otherwise, parse this line and add it to the comStack
            CharacterCommand lineCommand;
            try {
                lineCommand = ConvertLineToCommand(script[currentLine]);
            } catch (InvalidLineException e)
            {
                throw new InvalidLineException();
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new ForCommand(count, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException();
        }
    }


    private ConditionalStatement ParseConditionalStatement(string[] statementWords)
    {   
        if (statementWords.Length == 1) {
            // This means there is only one word in the statement,
            // which means the statement has to be one of the following
            switch (statementWords[0])
            {
                case "grounded":
                    return new PlayerGroundedStatement(true);
                case "!grounded":
                    return new PlayerGroundedStatement(false);
                default:
                    throw new InvalidLineException();
            }
        }
        
        // If the conditional statement isnt a single word,
        // Then it must be a FloatComparisonStatement of the following format:
        // CSV ComparisonSymbol CSV
        ConditionalStatementValue csv1, csv2;
        ComparisonType comp;
        

        // Parsing value for first CSV
        try {
            csv1 = ParseCSV(statementWords[0]);
        } catch (Exception e) {
            throw new InvalidLineException();
        }

        // Parsing value for comparison type
        try {
            comp = ParseComparisonType(statementWords[1]);
        } catch (Exception e) {
            throw new InvalidLineException();
        }

        // Parsing value for second CSV
        try {
            csv2 = ParseCSV(statementWords[2]);
        } catch (Exception e) {
            throw new InvalidLineException();
        }

        return new FloatComparisonStatement(csv1, csv2, comp);
    }

    private ConditionalStatementValue ParseCSV(string str)
    {
        switch (str)
        {
            case "x":
            case "horizontal":
                return new PlayerPositionCSV(true);
            case "y":
            case "vertical":
                return new PlayerPositionCSV(false);
            case "time":
                return new SimulationTimeCSV();
            default:
                float value = 0;
                try {
                    value = float.Parse(str);
                } catch (Exception e)
                {
                    throw new InvalidLineException();
                }
                return new FloatCSV(value);
        }
    }

    private ComparisonType ParseComparisonType(string str)
    {
        switch (str)
        {
            case ">":
                return ComparisonType.GT;
            case ">=":
                return ComparisonType.GTEQ;
            case "=":
            case "==":
                return ComparisonType.EQ;
            case "<=":
                return ComparisonType.LTEQ;
            case "<":
                return ComparisonType.LT;
            default:
                throw new InvalidLineException();
        }
    }
}
