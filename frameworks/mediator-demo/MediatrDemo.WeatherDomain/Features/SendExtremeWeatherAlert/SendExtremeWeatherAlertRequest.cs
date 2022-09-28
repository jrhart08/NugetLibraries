using MediatR;

namespace MediatrDemo.WeatherDomain.Features.SendExtremeWeatherAlert;

public readonly record struct SendExtremeWeatherAlertRequest(
    IEnumerable<string> ZipCodes,
    string AlertMessage
) : IRequest<SendExtremeWeatherAlertResponse>;
    
public readonly record struct SendExtremeWeatherAlertResponse(
    List<string> NotifiedPhoneNumbers);