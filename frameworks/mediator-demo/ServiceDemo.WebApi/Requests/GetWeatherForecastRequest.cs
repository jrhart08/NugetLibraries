namespace ServiceDemo.WebApi.Requests;

public readonly record struct GetWeatherForecastRequest(
    string ZipCode,
    int Days,
    bool IncludeSummaries = false);
    
public readonly record struct GetWeatherForecastByZipCodeRequest(
    IEnumerable<string> ZipCodes,
    int Days,
    bool IncludeSummaries = false);
    
public readonly record struct SendExtremeWeatherAlertsRequest(
    IEnumerable<string> ZipCodes,
    string AlertMessage);