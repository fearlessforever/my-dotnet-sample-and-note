using FluentValidation;
namespace Fearlessforever.Api.Core.FluentValidation;

public static partial class ApplicationServiceExtensions
{
  public static void AddCoreFluentValidation(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining<IMyWebApi>();
  }
}