using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using MiniProject.Modules.Events.Application.Events;
using MiniProject.Modules.Events.Application.Events.GetEventTest;

namespace MiniProject.Modules.Events.Presentation.Events;
internal static class GetEventTest
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async (
            string id,
            ISender sender,
            IValidator<GetEventTestQuery> validator) =>
        {
            var query = new GetEventTestQuery(id);

            ValidationResult validationResult = await validator.ValidateAsync(query);
            if (!validationResult.IsValid)
            {
                return Results.BadRequest("The id is invalid");
            }

            EventResponse? response = await sender.Send(query);

            return response is null ? Results.BadRequest("The id was not found") : Results.Ok(response);
        }).WithTags(Tags.Events);
    }
}
