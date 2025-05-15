using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProjects.Common.Messaging.Contracts.gRPC;

namespace MiniProject.Modules.Events.Presentation.Saga;
public sealed class CreateEventSagaOrchestrator
{
    private readonly List<ISagaStep> _steps = new();

    public void AddStep(ISagaStep step) => _steps.Add(step);

    public async Task<Result<string>> ExecuteSagaAsync(CreateEventRequest createEventRequest, CompensateEventRequest compensateEventRequest)
    {
        var executedSteps = new Stack<ISagaStep>();

        try
        {
            foreach (ISagaStep step in _steps)
            {
                CreateEventResponse response = await step.ExecuteAsync(createEventRequest);

                compensateEventRequest.EventId = response.EventId;
                executedSteps.Push(step);
            }

            return Result.Success(createEventRequest.Id);
        }
        catch (Exception ex)
        {
            while (executedSteps.Count > 0)
            {
                ISagaStep step = executedSteps.Pop();
                await step.CompensateAsync(compensateEventRequest);
            }

            return Result.Failure<string>(
                new Error("Saga",$"[Saga] Failed to process {createEventRequest}: {ex.Message}", ErrorType.Failure)
                );
        }
    }
}
