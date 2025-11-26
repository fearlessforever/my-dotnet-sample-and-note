using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabaseSqlite)]
public sealed class SqliteKeyGuidItemService(AppSqliteContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppSqliteContext, SqliteKeyGuidItem, Guid>(context, mapper, tokenProvider );