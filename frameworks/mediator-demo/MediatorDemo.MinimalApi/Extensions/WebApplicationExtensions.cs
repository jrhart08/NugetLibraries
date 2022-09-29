using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MediatorDemo.MinimalApi.Extensions;

public static class WebApplicationExtensions
{
    public static RouteHandlerBuilder MediateGet<TRequest>(this WebApplication app, string route)
    {
        return app.MapGet(route, async (IMediator mediator, [FromQuery] TRequest request) =>
        {
            return await mediator.Send(request);
        });
    }
}