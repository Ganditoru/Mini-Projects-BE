
namespace MiniProject.Modules.Events.Domain.Categories;

public interface ICategoryRepository
{
    void Insert(Category category);
    Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default);
}

