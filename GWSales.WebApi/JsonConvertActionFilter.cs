
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Text.Json;

namespace GWSales.WebApi;

public class JsonConvertActionFilter : IActionFilter, IOrderedFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.Result == null && !context.ModelState.IsValid
            && HasJsonErrors(context.ModelState, out var jsonException))
        {
            throw new JsonException(jsonException.Message);
        }
    }

    private static bool HasJsonErrors(ModelStateDictionary modelState, out Exception jsonException)
    {
        foreach (var entry in modelState.Values)
        {
            foreach (var error in entry.Errors)
            {
                if (error.Exception is JsonException)
                {
                    jsonException = error.Exception;
                    return true;
                }
            }
        }

        jsonException = null;
        return false;
    }

    public int Order => -1000000;
}
