using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Attendance.Domain.Attendance;

namespace MiniProject.Modules.Attendance.Infrastructure.Attendances;
internal sealed class AttendeeConfiguration : IEntityTypeConfiguration<Attendee>
{
    public void Configure(EntityTypeBuilder<Attendee> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(300);
    }
}
