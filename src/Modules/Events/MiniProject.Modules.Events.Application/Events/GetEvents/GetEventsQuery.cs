using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Events.GetEvents;
public sealed record GetEventsQuery : IRequest<Result<IReadOnlyCollection<EventResponse>>>;

