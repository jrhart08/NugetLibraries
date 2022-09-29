namespace CommonDependencies;

public interface ITwilioWrapper
{
    Task SendAlert(IEnumerable<string> phoneNumbers, string message);
}

public class UnimplementedTwilioWrapper : ITwilioWrapper
{
    public Task SendAlert(IEnumerable<string> phoneNumbers, string message)
    {
        throw new NotImplementedException();
    }
}