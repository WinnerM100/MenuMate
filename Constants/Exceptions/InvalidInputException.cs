namespace MenuMate.Constants.Exceptions;

public class InvalidInputException : Exception
{
    public InvalidInputException(string inputs)
        : base($"Invalid inputs detected. At least one input [{inputs}] must be not null.")
    {
    }
}