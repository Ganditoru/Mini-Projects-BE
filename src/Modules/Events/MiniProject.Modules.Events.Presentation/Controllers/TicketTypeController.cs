
using MiniProject.Modules.Events.Application.TicketTypes.GetTicketType;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Application.TicketTypes.GetTicketTypes;
using MiniProject.Modules.Events.Presentation.Abstraction;
using MiniProject.Modules.Events.Presentation.RequestDTOs.TicketType;
using MiniProject.Modules.Events.Application.TicketTypes.CreateTicketType;
using MiniProject.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;

namespace MiniProject.Modules.Events.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class TicketTypeController(ISender sender) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTicketTypes(Guid id)
    {
        Result<IReadOnlyCollection<TicketTypeResponse>> result = await sender.Send(new GetTicketTypesQuery(id));

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTicketType([FromBody] CreateTicketTypeRequest ticketType)
    {
        var createTicketTypeCommand = new CreateTicketTypeCommand(ticketType.EventId, ticketType.Name, ticketType.Price, ticketType.Currency, ticketType.Quantity);
        
        Result<Guid> result = await sender.Send(createTicketTypeCommand);
        
        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPut("{id}/price")]
    public async Task<IActionResult> UpdateTicketTypePrice(Guid id, [FromBody] UpdateTicketTypePriceRequest request)
    {
        var updateTicketTypePriceCommand = new UpdateTicketTypePriceCommand(id, request.Price);

        Result result = await sender.Send(updateTicketTypePriceCommand);

        return result.IsSuccess ? Ok() : ApiResults.Problem(result);
    }

}

