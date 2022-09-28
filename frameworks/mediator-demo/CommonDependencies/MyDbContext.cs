namespace CommonDependencies;

public class MyDbContext
{
    public async Task<List<string>> GetSupportedZipCodes()
    {
        return new();
    }

    public async Task<List<string>> GetSubscribersByZipCode(string zip)
    {
        return new();
    }
}