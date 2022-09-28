namespace CommonDependencies;

public interface ITwilioWrapper
{
    Task SendAlert(IEnumerable<string> phoneNumbers, string message);
}