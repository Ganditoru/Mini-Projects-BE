
using MiniProject.Modules.Attendance.Domain.Abstract;

namespace MiniProject.Modules.Attendance.Domain.Attendance;

public sealed class Attendee : Entity
{
    private Attendee()
    {
    }
    public Guid Id { get; private set; }

    public string Email { get; private set; }

    public string Name { get; private set; }

    public static Attendee Create(Guid id, string email, string name)
    {
        return new Attendee
        {
            Id = id,
            Email = email,
            Name = name
        };
    }

    public void Update(string name)
    {
        Name = name;
    }

}
