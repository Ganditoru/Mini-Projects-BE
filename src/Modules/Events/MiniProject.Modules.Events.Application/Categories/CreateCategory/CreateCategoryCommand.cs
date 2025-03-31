using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Categories.CreateCategory;
public sealed record CreateCategoryCommand(string Name) : IRequest<Result<Guid>>;
