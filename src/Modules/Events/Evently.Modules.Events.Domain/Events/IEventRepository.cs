namespace Evently.Modules.Events.Domain.Events;
public interface IEventRepository
{
    Task Insert(Event @event);

    Task<Event?> GetById(Guid id);
}
