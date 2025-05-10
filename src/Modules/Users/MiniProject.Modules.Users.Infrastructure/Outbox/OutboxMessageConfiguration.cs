using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Users.Domain.Outbox;

namespace MiniProject.Modules.Users.Infrastructure.Outbox;
public sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable("outbox_messages");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Content).HasMaxLength(2000).HasColumnType("jsonb");
    }
}
