using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniProject.Modules.Events.Domain.Categories;
using MiniProject.Modules.Events.Domain.Events;

namespace MiniProject.Modules.Events.Infrastructure.Events;
internal sealed class EventConfiguration : IEntityTypeConfiguration<Event>
{
    public void Configure(EntityTypeBuilder<Event> builder)
    {
        builder.HasOne<Category>().WithMany();
    }
}
