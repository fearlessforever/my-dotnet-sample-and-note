using System.Linq.Expressions;
using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services;

[FeatureFlag(Features.UseDatabaseMsSql)]
public sealed class BookCategoryService(AppMsSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider) :
BaseService<AppMsSqlContext, BookCategory, int>(context, mapper, tokenProvider)
{ 
  private readonly AppMsSqlContext context = context;
  private readonly CancelTokenProvider tokenProvider = tokenProvider;
  private readonly IMapper mapper = mapper;
  
  public async Task<T?> UpdateWithProjectTransactionAsync<T, TResource>(TResource resource , Expression<Func<BookCategory, bool>> predicateToGetId ) where TResource:class
  {
    using var trx = await context.Database.BeginTransactionAsync(tokenProvider.Token);
    try
    {
      var existingEntity = await GetByIdAsync<BookCategory>(predicateToGetId, ["Book"]);
      if (existingEntity is null) return default;

      existingEntity = mapper.Map(resource, existingEntity);
      context.Update(existingEntity);

      await context.SaveChangesAsync(tokenProvider.Token);
      await trx.CommitAsync(tokenProvider.Token);

      return mapper.Map<T>(existingEntity);
    }
    catch (Exception ex)
    {
      DebugHelper.LogError(ex.Message, "DB Transaction Process");
      await trx.RollbackAsync(tokenProvider.Token);

      return default;
    }
  }
}