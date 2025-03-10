
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetTest;
internal sealed class GetTestQueryHandler() : IRequestHandler<GetTestQuery, string>
{
    public async Task<string> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult($"API is working using Controller + CQRS! Received ID: {request.Id}");
    }
}
