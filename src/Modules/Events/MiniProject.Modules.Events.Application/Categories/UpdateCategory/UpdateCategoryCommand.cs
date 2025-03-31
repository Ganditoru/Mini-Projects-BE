
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Categories.UpdateCategory;
public sealed record UpdateCategoryCommand(Guid CategoryId, string Name) : IRequest<Result>;

