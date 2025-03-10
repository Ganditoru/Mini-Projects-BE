using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;

namespace Evently.Modules.Events.Infrastructure.Events;
internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public async Task<Event?> GetById(Guid id)
    {
        return await context.Events.FindAsync(id);
    }

    public async Task Insert(Event @event)
    {
        context.Events.Add(@event);
        await context.SaveChangesAsync();
    }
}
