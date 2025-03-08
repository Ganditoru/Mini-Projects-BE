using MediatR;

namespace MiniProject.Modules.Events.Application.Events.GetEventTest;
public sealed record GetEventTestQuery(string EventId): IRequest<EventResponse?>;
