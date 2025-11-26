

using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Core.AppCancelToken;

internal sealed class CancelTokenProviderMiddleware( CancelTokenProvider cancelTokenProvider ): IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    var linkedCancelToken = CancellationTokenSource.CreateLinkedTokenSource(context.RequestAborted , AppCancelTokenService.CancelTokenSource.Token);
    cancelTokenProvider.Token = linkedCancelToken.Token;
    await next(context);
  }
}