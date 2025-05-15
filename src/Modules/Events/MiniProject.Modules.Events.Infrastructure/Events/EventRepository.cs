using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Infrastructure.Database;

namespace MiniProject.Modules.Events.Infrastructure.Events;
internal sealed class EventRepository(EventsDbContext context) : IEventRepository
{
    public void Insert(Event @event)=> context.Events.Add(@event);

    public Task<Event?> GetAsync(Guid id, CancellationToken cancellationToken = default)
        => context.Events.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

    public void Delete(Event @event) => context.Events.Remove(@event);
}
