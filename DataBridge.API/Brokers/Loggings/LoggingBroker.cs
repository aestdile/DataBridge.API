namespace DataBridge.API.Brokers.Loggings;

public class LoggingBroker : ILoggingBroker
{
    private readonly ILogger<LoggingBroker> logger;

    public LoggingBroker(ILogger<LoggingBroker> logger) =>
        this.logger = logger;

    public void LogError(Exception exception) =>
        this.logger.LogError(exception, exception.Message);

    public void LogCritical(Exception exception) =>
        this.logger.LogCritical(exception, exception.Message);

    public void LogInformation(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            throw new ArgumentException("Message cannot be null or whitespace.", nameof(message));
        }
        this.logger.LogInformation(message);
    }

    public void LogWarning(Exception exception) =>
        this.logger.LogWarning(exception, exception.Message);
}