using System;

public class InvalidLineException : Exception
{
    private int lineNumber;
    private string message;

    public InvalidLineException(int lineNumber, string message) 
    {
        this.lineNumber = lineNumber + 1; // add one bc code parser starts at index 0
        this.message = message;
    }

    public string GetErrorText()
    {
        return "Error! Line " + lineNumber + ":\n" + message;
    }
}
