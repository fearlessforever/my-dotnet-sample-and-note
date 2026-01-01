using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabasePostgreSql)]
public sealed class ViewPostgreKeyService(AppPostgreSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider) :
BaseService<AppPostgreSqlContext, ViewPostgreKeyItem, string>(context, mapper, tokenProvider)
{
  private readonly AppPostgreSqlContext context = context;
  private readonly CancelTokenProvider tokenProvider = tokenProvider;
  private readonly IMapper mapper = mapper;

  public async Task<T?> GetByIdFromFunctionAsync<T>(Guid guidKey) where T : class
  {
#if NET8_0_OR_GREATER
      // var result = await context.Database.SqlQuery<T>(
      //     $"""
      //       SELECT * FROM get_data_guid_key({guidKey})
      //     """)
      //     .FirstOrDefaultAsync(tokenProvider.Token);
#endif

#if NET7_0
    // var result = await context.PostgreKeyGuidItems.FromSqlInterpolated(
    //     $"""
    //       SELECT * FROM get_data_guid_key({guidKey})
    //     """)
    //     .IgnoreQueryFilters()
    //     .ProjectToType<T>().FirstOrDefaultAsync(tokenProvider.Token);

    // NOTE: FOLLOWING DOES NOT WORK !!!

    // var result = await context.Database.SqlQueryRaw<PostgreKeyGuidItem>(
    //     @"
    //       SELECT * FROM get_data_guid_key({0})
    //     ", guidKey )
    //     .ProjectToType<T>().FirstOrDefaultAsync(tokenProvider.Token);

    // IQueryable<T> query = EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<T>().FromSql(
    //   $"""
    //     SELECT * FROM get_data_guid_key({guidKey})
    //   """
    // ));
    // var result = await query.FirstOrDefaultAsync();

    // var result = await context.Set<T>().FromSqlRaw(
    //     @"
    //       SELECT * FROM get_data_guid_key({0})
    //     ", guidKey)
    //     .AsNoTracking()  // Ensure no tracking is done, as it's not part of the model
    //     .FirstOrDefaultAsync(tokenProvider.Token);
#endif

    var resultData = await context.Database.SqlQuery<string>(
          $"""
            SELECT get_data_guid_key({guidKey}) as "Value"
          """)
          .FirstOrDefaultAsync(tokenProvider.Token);

    if (resultData is null) return default;

    var result = System.Text.Json.JsonSerializer.Deserialize<T>(resultData);

    return result;
  }

  public async Task<(Guid guid,string name,int result)> AddByStoreProcedureAsync(Guid guidKey  , string name)
  {
    string createdBy = "[Store Procedure]";
    byte[] bytesKey = guidKey.ToByteArray();

    var result = await context.Database.ExecuteSqlAsync(
      $"""
      CALL insert_into_PostgreKeyItems({guidKey},{bytesKey},{name},{createdBy})
      """
    , tokenProvider.Token );

    return (guidKey,name,result);
  }
}