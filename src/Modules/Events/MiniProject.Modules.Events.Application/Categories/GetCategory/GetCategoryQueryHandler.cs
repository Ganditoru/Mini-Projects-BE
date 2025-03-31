using System.Data.Common;
using Dapper;
using MediatR;
using MiniProject.Modules.Events.Application.Abstractions;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Domain.Categories;

namespace MiniProject.Modules.Events.Application.Categories.GetCategory;
internal sealed class GetCategoryQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetCategoryQuery, Result<CategoryResponse>>
{
    public async Task<Result<CategoryResponse>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(CategoryResponse.Id)},
                 name AS {nameof(CategoryResponse.Name)},
                 is_archived AS {nameof(CategoryResponse.IsArchived)}
             FROM events2.categories
             WHERE id = @CategoryId
             """;

        CategoryResponse? category = await connection.QuerySingleOrDefaultAsync<CategoryResponse>(sql, request);

        if (category is null)
        {
            return Result.Failure<CategoryResponse>(CategoryErrors.NotFound(request.CategoryId));
        }

        return category;
    }
}
