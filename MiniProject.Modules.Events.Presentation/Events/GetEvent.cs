
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Events.Application.Events;
using MiniProject.Modules.Events.Application.Events.GetEvent;

namespace MiniProject.Modules.Events.Presentation.Events;
internal static class GetEvent 
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async (Guid id, ISender sender, CancellationToken cancellationToken) =>
        {
            var query = new GetEventQuery(id);

            // Send message
            EventResponse? response = await sender.Send(query, cancellationToken);

            return response is null ? Results.NotFound("The user could not be found") : Results.Ok(response);
        }).WithTags(Tags.Events);
    }
}
