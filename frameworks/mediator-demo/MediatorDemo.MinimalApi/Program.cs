using CommonDependencies;
using MediatorDemo.MinimalApi.Extensions;
using MediatR;
using MediatrDemo.WeatherDomain;
using MediatrDemo.WeatherDomain.Features.GetWeatherForecast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<ITwilioWrapper, UnimplementedTwilioWrapper>();
builder.Services.AddTransient<MyDbContext>();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblyContaining<DomainRef>();
});

var app = builder.Build();

app.MediateGet<GetWeatherForecastResponse>("api/weather");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.Run();