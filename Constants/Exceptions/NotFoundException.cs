namespace MenuMate.Constants.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, string details = "")
        : base($"No {entity} found in database, based on current query. Query details: {details}")
    {
    }
}