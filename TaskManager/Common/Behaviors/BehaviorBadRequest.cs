using Microsoft.AspNetCore.Mvc;

namespace TaskManager.Common.Behaviors;

public class BehaviorBadRequest
{
    public static void ParseModelErrors(ApiBehaviorOptions options)
    {
        options.InvalidModelStateResponseFactory = (context) =>
        {
            List<string> responseError = new();
            if (!context.ModelState.IsValid)
            {
                responseError = context.ModelState.Values.Where(v => v.Errors.Count > 0)
                        .SelectMany(v => v.Errors)
                        .Select(v => v.ErrorMessage)
                        .ToList();
            }
            return new BadRequestObjectResult(responseError);
        };
    }
}
