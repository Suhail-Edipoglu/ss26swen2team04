using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SWEN2TourPlanner.Api.Endpoints;

public class ValidationFilter<T> : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var argToValidate = context.Arguments.FirstOrDefault(a => a is T);
        if (argToValidate != null)
        {
            var validationContext = new ValidationContext(argToValidate);
            var validationResults = new List<ValidationResult>();
            if (!Validator.TryValidateObject(argToValidate, validationContext, validationResults, true))
            {
                var errors = validationResults
                    .GroupBy(e => e.MemberNames.FirstOrDefault() ?? string.Empty)
                    .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage ?? "Validation error").ToArray());
                return TypedResults.ValidationProblem(errors);
            }
        }
        return await next(context);
    }
}
