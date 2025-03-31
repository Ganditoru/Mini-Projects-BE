using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.CreateEvent;

public sealed record CreateEventCommand(Guid CategoryId, string Title, string Description, string Location, DateTime StartsAtUtc, DateTime? EndsAtUtc) : IRequest<Result<Guid>>;
