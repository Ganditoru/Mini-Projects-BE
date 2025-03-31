using System.ComponentModel.DataAnnotations;

namespace MiniProject.Modules.Events.Presentation.RequestDTOs.TicketType;
public sealed record UpdateTicketTypePriceRequest(
    [Required] Guid TicketTypeId,
    [Range(1, 9999999)] decimal Price
);
