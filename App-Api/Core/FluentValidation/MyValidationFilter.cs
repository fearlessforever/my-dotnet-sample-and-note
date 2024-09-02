
using Fearlessforever.Api.Utils;
using FluentValidation;

namespace Fearlessforever.Api.Core.FluentValidation;

public class MyValidationFilter<T>(IValidator<T> validator) : IEndpointFilter
{
  // public async ValueTask<object?> In
  public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
  {
    try
    {
      var request = context.Arguments.OfType<T>().First();
      var result = await validator.ValidateAsync(request, context.HttpContext.RequestAborted);
      return result.IsValid ? await next(context) : MyApiResponse.GenerateApiResponse<object>(null, message:"Bad Request", status: "error" , isHeaderStatus: true , code:400, errors: result.ToDictionary() );
    }
    catch (InvalidOperationException ex)
    {
      Type typeValidator = typeof(T);
      IEnumerable<string> errorResult = [];
      errorResult = errorResult.Prepend( ex.Message );
      errorResult = errorResult.Prepend($"Filter Validator: {typeValidator.Name}");
      return MyApiResponse.GenerateApiResponse<object>(null, message: "Bad Request", status: "error", isHeaderStatus: true, code: 500, errors: errorResult);
    }
  }
}