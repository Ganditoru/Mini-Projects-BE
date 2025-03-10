using MediatR;

namespace Evently.Modules.Events.Application.Events.GetTest;
public sealed record GetTestQuery(string Id): IRequest<string>;
