

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;
public static class EventsEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("/api", () => new { response = "It Works" });
    }

}
