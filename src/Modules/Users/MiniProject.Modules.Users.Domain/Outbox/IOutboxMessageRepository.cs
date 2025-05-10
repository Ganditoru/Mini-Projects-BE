
namespace MiniProject.Modules.Users.Domain.Outbox;
public interface IOutboxMessageRepository
{
    void Insert(OutboxMessage outboxMessage);
}
