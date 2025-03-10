using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Infrastructure.Database;

namespace MiniProject.Modules.Events.Infrastructure.Events;
internal sealed class EventRepository(EventsDbContext context): IEventRepository
{
    public void Insert(Event @event)
    {
        context.Events.Add(@event);
    }
}
