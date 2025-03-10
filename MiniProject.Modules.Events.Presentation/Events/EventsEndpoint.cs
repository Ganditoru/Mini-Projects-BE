using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MiniProject.Modules.Events.Presentation.Events;

public static class EventsEndpoint
{

    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api", () => new { response = "It Works" }).WithTags("Default");
        

        GetEventTest.MapEndpoint(app);
        GetEvent.MapEndpoint(app);

        CreateEvent.MapEndpoint(app);
    }
}

