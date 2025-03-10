using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Events.Application.Events.CreateEvent;

namespace MiniProject.Modules.Events.Presentation.Events;
internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (CreateEventRequest request, ISender sender, [FromServices] IValidator<CreateEventCommand> validator) =>
        {
            var command = new CreateEventCommand(request.Title, request.Description, request.Location);

            ValidationResult validate = await validator.ValidateAsync(command);

            if (!validate.IsValid)
            {
                return Results.BadRequest("Id is not valid");
            }

            Guid eventId = await sender.Send(command);

            return Results.Ok(eventId);
        }).WithTags(Tags.Events);
    }

    internal sealed class CreateEventRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Location { get; set; }
    }
}
