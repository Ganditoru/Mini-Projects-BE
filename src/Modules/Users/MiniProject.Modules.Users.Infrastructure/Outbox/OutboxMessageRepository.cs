using MiniProject.Modules.Users.Domain.Outbox;
using MiniProject.Modules.Users.Infrastructure.Database;

namespace MiniProject.Modules.Users.Infrastructure.Outbox;
internal sealed class OutboxMessageRepository(UserDbContext context) : IOutboxMessageRepository
{

    public void Insert(OutboxMessage outboxMessage)
    {
        context.OutboxMessage.Add(outboxMessage);
    }
}
