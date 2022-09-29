using MiscLibraries.Flurl;
using MiscLibraries.Hashids;
using MiscLibraries.Polly;
using MiscLibraries.LazyCacheEx;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddTransient<FlurlExamples>(provider =>
{
    var config = provider.GetService<IConfiguration>()!;
    var httpClientFactory = provider.GetService<IHttpClientFactory>()!;

    return new(config["OpenWeatherApiKey"]!, httpClientFactory);
});
builder.Services.AddTransient<RetryExamples>();
builder.Services.AddSingleton<CircuitBreakerExamples>();
builder.Services.AddSingleton<HashService>();
builder.Services.AddTransient<LazyCacheExamples>();

var app = builder.Build();

app.MapGet(
    "api/weather/realweather",
    (FlurlExamples fe, double lat, double lon) => fe.GetWeatherObject(lat, lon));

app.MapGet(
    "api/weather/stubborn",
    (RetryExamples re, double? lat, double? lon) => re.StubbornlyGetWeather(lat, lon));

app.MapGet(
    "api/weather/cautious",
    (CircuitBreakerExamples ce, double? lat, double? lon) => ce.CautiouslyGetWeather(lat, lon));

app.MapGet(
    "api/weather/cached",
    (LazyCacheExamples lce, double lat, double lon) => lce.GetCached(lat, lon));

app.MapGet(
    "api/hashids/encode",
    (HashService hs, Guid id) => hs.Encode(id));

app.MapGet(
    "api/hashids/decode",
    (HashService hs, string hash) => hs.Decode(hash));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();
//
// app.MapControllers();

app.Run();