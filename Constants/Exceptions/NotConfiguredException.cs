
namespace MenuMate.Constants.Exceptions;

public class NotConfiguredException: Exception
{
    public NotConfiguredException(string missingConfigurationName): base($"{missingConfigurationName} configuration was not set or not found in root appsettings config file!")
    {
        
    }
}