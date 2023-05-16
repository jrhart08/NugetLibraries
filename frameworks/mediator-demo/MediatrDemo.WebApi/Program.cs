using CommonDependencies;
using MediatR;
using MediatrDemo.WeatherDomain;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();