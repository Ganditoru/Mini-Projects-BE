
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Categories.ArchiveCategory;
public sealed record ArchiveCategoryCommand(Guid CategoryId) : IRequest<Result>;

