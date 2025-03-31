
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Categories.GetCategory;
public sealed record GetCategoryQuery(Guid CategoryId) : IRequest<Result<CategoryResponse>>;

