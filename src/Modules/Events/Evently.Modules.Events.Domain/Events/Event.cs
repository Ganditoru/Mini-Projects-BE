namespace Evently.Modules.Events.Domain.Events;

public sealed record Event
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public string Location { get; set; }
}

