using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Events.Domain.Events;
using MiniProject.Modules.Events.Domain.TicketTypes;

namespace MiniProject.Modules.Events.Infrastructure.TicketTypes;
internal sealed class TicketTypeConfiguration : IEntityTypeConfiguration<TicketType>
{
    public void Configure(EntityTypeBuilder<TicketType> builder)
    {
        builder.HasOne<Event>().WithMany().HasForeignKey(t => t.EventId);
    }
}
