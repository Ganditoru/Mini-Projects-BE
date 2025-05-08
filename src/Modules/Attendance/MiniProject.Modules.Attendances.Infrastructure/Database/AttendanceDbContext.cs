
using Microsoft.EntityFrameworkCore;
using MiniProject.Modules.Attendance.Application.Abstract;
using MiniProject.Modules.Attendance.Domain.Attendance;
using MiniProject.Modules.Attendance.Infrastructure.Attendances;

namespace MiniProject.Modules.Attendance.Infrastructure.Database;
public sealed class AttendanceDbContext(DbContextOptions<AttendanceDbContext> option): DbContext(option), IUnitOfWork
{
    internal DbSet<Attendee> Attendees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Attendance);

        modelBuilder.ApplyConfiguration(new AttendeeConfiguration());
    }

}
