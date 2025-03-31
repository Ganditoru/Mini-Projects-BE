using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MiniProject.Modules.Events.Presentation.Abstraction;


[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public sealed class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        // If the ModelState is invalid, create a ValidationProblemDetails response.
        if (!context.ModelState.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState)
            {
                Title = "One or more validation errors occurred.",
                Status = StatusCodes.Status400BadRequest
            };

            // Return 400 Bad Request with problem details
            context.Result = new BadRequestObjectResult(problemDetails);
        }
    }
}
