namespace MiniProject.Modules.Events.Application.Abstractions;
public interface ISagaStep<TExecuteReq, TExecuteRes, TCompReq, TCompRes>
{
    Task<TExecuteRes> ExecuteAsync(TExecuteReq request);
    Task<TCompRes> CompensateAsync(TCompReq request);
}
