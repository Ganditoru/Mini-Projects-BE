using MediatR;
using Microsoft.AspNetCore.Mvc;
using MiniProject.Modules.Events.Application.Categories.ArchiveCategory;
using MiniProject.Modules.Events.Application.Categories.CreateCategory;
using MiniProject.Modules.Events.Application.Categories.GetCategories;
using MiniProject.Modules.Events.Application.Categories.GetCategory;
using MiniProject.Modules.Events.Application.Categories.UpdateCategory;
using MiniProject.Modules.Events.Domain.Abstractions;
using MiniProject.Modules.Events.Presentation.Abstraction;
using MiniProject.Modules.Events.Presentation.RequestDTOs.Categories;

namespace MiniProject.Modules.Events.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ISender sender) : ControllerBase
{

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var query = new GetCategoriesQuery();

        Result<IReadOnlyCollection<CategoryResponse>> result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var query = new GetCategoryQuery(id);

        Result<CategoryResponse> result = await sender.Send(query);

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest createCategoryRequest)
    {
        var command = new CreateCategoryCommand(createCategoryRequest.Name);

        Result<Guid> result = await sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : ApiResults.Problem(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryRequest updateCategoryRequest)
    {
        var command = new UpdateCategoryCommand(id, updateCategoryRequest.Name);

        Result result = await sender.Send(command);

        return result.IsSuccess ? Ok(result) : ApiResults.Problem(result);
    }

    [HttpPost("arhive")]
    public async Task<IActionResult> ArhiveCategory(Guid id)
    {
        var command = new ArchiveCategoryCommand(id);

        Result result = await sender.Send(command);

        return result.IsSuccess ? Ok(result) : ApiResults.Problem(result);
    }
}

