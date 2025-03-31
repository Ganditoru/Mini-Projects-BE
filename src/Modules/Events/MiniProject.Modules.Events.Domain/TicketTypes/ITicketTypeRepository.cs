namespace MiniProject.Modules.Events.Domain.TicketTypes;
public interface ITicketTypeRepository
{
    void Insert(TicketType ticketType);
    Task<TicketType?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid eventId, CancellationToken cancellationToken = default);
}
