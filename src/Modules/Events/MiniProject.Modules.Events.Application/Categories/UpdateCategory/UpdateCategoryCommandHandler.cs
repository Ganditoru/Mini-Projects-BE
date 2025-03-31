using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Categories;

namespace MiniProject.Modules.Events.Application.Categories.UpdateCategory;
internal sealed class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCategoryCommand, Result>
{
    public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? category = await categoryRepository.GetAsync(request.CategoryId, cancellationToken);

        if (category is null)
        {
            return Result.Failure(CategoryErrors.NotFound(request.CategoryId));
        }

        category.ChangeName(request.Name);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
