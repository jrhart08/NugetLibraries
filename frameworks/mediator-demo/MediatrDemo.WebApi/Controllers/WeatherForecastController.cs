using MediatR;
using MediatrDemo.WeatherDomain.Features.GetSupportedZipCodes;
using MediatrDemo.WeatherDomain.Features.GetWeatherForecast;
using MediatrDemo.WeatherDomain.Features.GetWeatherForecastByZipCode;
using MediatrDemo.WeatherDomain.Features.SendExtremeWeatherAlert;
using Microsoft.AspNetCore.Mvc;

namespace MediatrDemo.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    readonly IMediator _mediator;

    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public async Task<GetWeatherForecastResponse> Get(
        [FromQuery] GetWeatherForecastRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("byzipcode")]
    public async Task<GetWeatherForecastByZipCodeResponse> GetByZipCode(
        [FromQuery] GetWeatherForecastByZipCodeRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet("zipcodes")]
    public async Task<GetSupportedZipCodesResponse> GetSupportedZipCodes()
    {
        return await _mediator.Send(new GetSupportedZipCodesRequest());
    }

    [HttpPost("alert")]
    public async Task<SendExtremeWeatherAlertResponse> SendExtremeWeatherAlert(
        [FromBody] SendExtremeWeatherAlertRequest request)
    {
        return await _mediator.Send(request);
    }
}
