namespace MiniProject.Modules.Events.Presentation.RequestDTOs;
public sealed class RescheduleEventRequest
{
    public DateTime StartsAtUtc { get; init; }

    public DateTime? EndsAtUtc { get; init; }
}
