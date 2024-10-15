using System;

public class InvalidLineException : Exception
{
    public InvalidLineException() : base("Line of code is not valid") { }
}
