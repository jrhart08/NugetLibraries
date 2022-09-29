using BasicMediatrDemo.Handlers.GetWeatherForecast;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BasicMediatrDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public WeatherForecastController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<GetWeatherForecastResponse> Get(GetWeatherForecastRequest request)
    {
        return await _mediator.Send(request);
    }
}