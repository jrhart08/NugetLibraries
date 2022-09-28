using CommonDependencies;
using MediatR;

namespace MediatrDemo.WeatherDomain.Features.SendExtremeWeatherAlert;

public class SendExtremeWeatherAlertHandler : IRequestHandler<SendExtremeWeatherAlertRequest, SendExtremeWeatherAlertResponse>
{
    readonly MyDbContext _context;
    readonly ITwilioWrapper _twilio;

    public SendExtremeWeatherAlertHandler(MyDbContext context, ITwilioWrapper twilio)
    {
        _context = context;
        _twilio = twilio;
    }

    public async Task<SendExtremeWeatherAlertResponse> Handle(SendExtremeWeatherAlertRequest request, CancellationToken cancellationToken)
    {
        var (zipCodes, alertMessage) = request;
        
        List<string> phoneNumbersToAlert = _context
            .SupportedAreas
            .Where(area => zipCodes.Contains(area.ZipCode))
            .SelectMany(area => area.Subscribers)
            .Select(sub => sub.PhoneNumber)
            .ToList();

        await _twilio.SendAlert(phoneNumbersToAlert, alertMessage);

        return new(phoneNumbersToAlert);
    }
}