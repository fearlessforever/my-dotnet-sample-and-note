
using Fearlessforever.Api.Utils;
using FluentValidation;

namespace Fearlessforever.Api.Core.FluentValidation;

public class MyValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
  // public async ValueTask<object?> In
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    var request = context.Arguments.OfType<T>().First();

    var result = await validator.ValidateAsync(request);

    return result.IsValid ? await next(context) : MyApiResponse.GenerateApiResponse<object>(null, message:"Bad Request", status: "error" , isHeaderStatus: true , code:400, errors: result.ToDictionary() );
  }
}