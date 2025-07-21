namespace DataBridge.API.Brokers.Loggings;

public interface ILoggingBroker
{
    void LogInformation(string message);
    void LogWarning(Exception exception);
    void LogError(Exception exception);
    void LogCritical(Exception exception);
}