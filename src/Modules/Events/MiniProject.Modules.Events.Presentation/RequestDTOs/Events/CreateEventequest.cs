using System.ComponentModel.DataAnnotations;

namespace MiniProject.Modules.Events.Presentation.RequestDTOs.Events;


public class CreateEventequest
{
    [Required(ErrorMessage = "CategoryId is required.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Title is required.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "Title must be between 3 and 100 characters.")]
    public string Title { get; set; } = default!;

    [Required(ErrorMessage = "Description is required.")]
    [StringLength(1000, ErrorMessage = "Description can't exceed 1000 characters.")]
    public string Description { get; set; } = default!;

    [Required(ErrorMessage = "Location is required.")]
    [StringLength(200, ErrorMessage = "Location can't exceed 200 characters.")]
    public string Location { get; set; } = default!;

    [Required(ErrorMessage = "Start date/time is required.")]
    public DateTime StartsAtUtc { get; set; }

    public DateTime? EndsAtUtc { get; set; }
}
