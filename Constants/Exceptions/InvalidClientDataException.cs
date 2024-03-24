namespace MenuMate.Constants.Exceptions;

public class InvalidClientDataException : Exception
{
    public InvalidClientDataException(string property)
        : base($"Invalid client data in query! '{property}' property is null or has not a valid format.")
    {
    }
}