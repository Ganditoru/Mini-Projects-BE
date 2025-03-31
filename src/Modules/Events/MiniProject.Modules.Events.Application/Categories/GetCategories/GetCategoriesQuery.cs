using MiniProject.Modules.Events.Application.Categories.GetCategory;
using MediatR;
using MiniProject.Modules.Events.Domain.Abstractions;

namespace MiniProject.Modules.Events.Application.Categories.GetCategories;
public sealed record GetCategoriesQuery : IRequest<Result<IReadOnlyCollection<CategoryResponse>>>;

