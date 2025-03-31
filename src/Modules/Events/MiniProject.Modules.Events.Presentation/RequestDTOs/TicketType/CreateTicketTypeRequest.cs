
using System.ComponentModel.DataAnnotations;

namespace MiniProject.Modules.Events.Presentation.RequestDTOs.TicketType;
public sealed record CreateTicketTypeRequest(
    [Required] Guid EventId,
    [Required, MaxLength(100)] string Name,
    [Range(1, 9999999)] decimal Price,
    [Required, StringLength(3, MinimumLength = 3)] string Currency,
    [Range(1, 9999999)] decimal Quantity
);
