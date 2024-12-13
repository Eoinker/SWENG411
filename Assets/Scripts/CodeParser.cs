using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CodeParser : MonoBehaviour
{
    public delegate void LineErrorHandler(string e);
    public static event LineErrorHandler OnLineError;
    public List<string> script;
    public int currentLine;

    private CharacterCommandManager ccm;
    private TMP_InputField codeInputField;

    private void Start()
    {
        SimulationUIManager simUI = FindAnyObjectByType<SimulationUIManager>();
        codeInputField = simUI.codeInputField;
        ccm = GetComponent<CharacterCommandManager>();
    }

    public bool CompileCodeInput() {
        SetScript(codeInputField.text);
        ccm.StopExecuting();
        Queue<CharacterCommand> commandQueue;
        try {
            commandQueue = ConvertScriptToCommandQueue();
        } catch (InvalidLineException e)
        {
            OnLineError?.Invoke(e.GetErrorText());
            return false;
        }
        ccm.SetCommandQueue(commandQueue);
        return true;
    }

    public void RunCodeInput()
    {
        CompileCodeInput();
        Debug.Log("Compiling Complete. Starting Execution!");
        ccm.BeginExecution();
    }

    private void SetScript(string rawCode) {
        
        script.Clear();
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
                    script.Add(trimmedLine.ToLower());
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
                throw e;
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
            throw new InvalidLineException(currentLine, "This error should never happen!");
        }

        string[] split = line.Split(' ');

        switch (split[0])
        {
            case "move":
                try {
                    return ParseMoveCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            case "wait":
                try {
                    return ParseWaitCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            case "jump":
                try {
                    return ParseJumpCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            case "if":
                try {
                    return ParseIfCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            case "while":
                try {
                    return ParseWhileCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            case "for":
                try {
                    return ParseForCommand(split);
                } catch (InvalidLineException e) {
                    throw e;
                }
            default:
                throw new InvalidLineException(currentLine, "'" + split[0] + "' is not a recognized command.");
        }
    }

    private CharacterCommand ParseMoveCommand(string[] splitLine)
    {
        // Move command only takes one additional word after
        if (splitLine.Length != 2)
        {
            throw new InvalidLineException(currentLine, "The move command requires exactly one parameter.");
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
                throw new InvalidLineException(currentLine, "'" + splitLine[1] + "' is not a recognized parameter for the move command.");
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
        } catch 
        {
            throw new InvalidLineException(currentLine, "Unable to convert '" + splitLine[1] + "' to a float.");
        }

        return new WaitForSecondsCommand(seconds);
    }

    private CharacterCommand ParseJumpCommand(string[] splitLine)
    {
        // if there is more after "jump" 
        if (splitLine.Length > 1)
        {
            throw new InvalidLineException(currentLine, "The jump command does take any parameters.");
        }

        return new JumpCommand();
    }

    private CharacterCommand ParseIfCommand(string[] splitLine)
    {
        // Checking if there is anything after if
        if (splitLine.Length == 1)
        {
            // There cant be nothing after the if
            throw new InvalidLineException(currentLine, "The if command requires additional parameters.");
        }

        // Keeping track of where the if statement starts
        int ifStatementLineNum = currentLine;

        ConditionalStatement cond;
        try {
            cond = ParseConditionalStatement(splitLine.Skip(1).ToArray());
        } catch (InvalidLineException e) {
            throw e;
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
                throw e;
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new IfCommand(cond, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException(ifStatementLineNum, "This if statement has no end.");
        }
    }

    private CharacterCommand ParseWhileCommand(string[] splitLine)
    {
        // Checking if there is anything after while
        if (splitLine.Length == 1)
        {
            // There cant be nothing after the while
            throw new InvalidLineException(currentLine, "The while command requires additional parameters.");
        }

        // Keeping track of where the while loop starts
        int whileLoopLineNum = currentLine;

        ConditionalStatement cond;
        try {
            cond = ParseConditionalStatement(splitLine.Skip(1).ToArray());
        } catch (InvalidLineException e) {
            throw e;
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
                throw e;
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new WhileCommand(cond, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException(whileLoopLineNum, "This while loop has no end.");
        }
    }

    private CharacterCommand ParseForCommand(string[] splitLine)
    {
        if (splitLine.Length != 2)
        {
            // For loops must have exactly thing after them
            throw new InvalidLineException(currentLine, "The for command requires exactly one parameter.");
        }
        // Keeping track of where the for loop starts
        int forLoopLineNum = currentLine;

        // Parsing the number of iterations to run
        int count = 0;
        try {
            count = int.Parse(splitLine[1]);
        } catch
        {
            throw new InvalidLineException(currentLine, "Unable to convert '" + splitLine[1] + "' to an integer.");
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
                throw e;
            }

            comStack.Push(lineCommand);
            currentLine++;
        }

        if (foundEnd == true)
        {
            return new ForCommand(count, new Stack<CharacterCommand>(comStack));
        } else {
            throw new InvalidLineException(forLoopLineNum, "This for loop has no end.");
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
                case "true":
                    return new SimpleBooleanStatement(true);
                case "false":
                    return new SimpleBooleanStatement(false);
                case "completed":
                case "complete":
                    return new LevelCompletedStatement(true);
                case "!completed":
                case "!complete":
                    return new LevelCompletedStatement(false);
                default:
                    throw new InvalidLineException(currentLine, "'" + statementWords[0] + "' is not a recognized boolean variable.");
            }
        } else if (statementWords.Length == 3) {
            // If the conditional statement isnt a single word,
            // Then it must be a FloatComparisonStatement of the following format:
            // CSV ComparisonSymbol CSV
            ConditionalStatementValue csv1, csv2;
            ComparisonType comp;
            

            // Parsing value for first CSV
            try {
                csv1 = ParseCSV(statementWords[0]);
            } catch (InvalidLineException e) {
                throw e;
            }

            // Parsing value for comparison type
            try {
                comp = ParseComparisonType(statementWords[1]);
            } catch (InvalidLineException e) {
                throw e;
            }

            // Parsing value for second CSV
            try {
                csv2 = ParseCSV(statementWords[2]);
            } catch (InvalidLineException e) {
                throw e;
            }

            return new FloatComparisonStatement(csv1, csv2, comp);
        } else {
            // If the conditional statement does not match one of the above forms then it is invalid.
            throw new InvalidLineException(currentLine, "Conditional statements following if or while commands must be one of two forms: a single recognized boolean variable (i.e. grounded), or a comparison of recognized float variables (i.e time < 3).");
        }
        
        
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
                } catch
                {
                    throw new InvalidLineException(currentLine, "Unable to convert '" + str + "' to a float, and it is not a recognized variable.");
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
                throw new InvalidLineException(currentLine, "'" + str + "' is not a recognized comparison operator.");
        }
    }
}