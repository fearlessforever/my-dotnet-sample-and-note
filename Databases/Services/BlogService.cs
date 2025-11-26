using System.Linq.Expressions;
using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services;

[FeatureFlag(Features.UseDatabaseSqlite)]
public sealed class BlogService(AppSqliteContext context, IMapper mapper, CancelTokenProvider tokenProvider) :
BaseService<AppSqliteContext, Blog, int>(context, mapper, tokenProvider)
{
  private readonly AppSqliteContext context = context;
  private readonly CancelTokenProvider tokenProvider = tokenProvider;
  private readonly IMapper mapper = mapper;
  public async Task<T?> UpdateWithAuthorTransactionAsync<T, TResource>(TResource resource , Expression<Func<Blog, bool>> predicateToGetId ) where TResource:class
  {
    using var trx = await context.Database.BeginTransactionAsync(tokenProvider.Token);
    try
    {
      var existingEntity = await GetByIdAsync<Blog>(predicateToGetId, ["Author"]);
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