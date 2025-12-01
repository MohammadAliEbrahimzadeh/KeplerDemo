using KeplerDemo.Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Kepler.Web.Filters;

public class StatusCodeActionFilter : IActionFilter, IFilterMetadata
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult { Value: CustomResponse value } objectResult)
        {
            objectResult.StatusCode = (int)value.StatusCode;
            objectResult.DeclaredType = typeof(CustomResponse);
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
    }
}
