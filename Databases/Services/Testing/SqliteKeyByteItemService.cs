using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabaseSqlite)]
public sealed class SqliteKeyByteItemService(AppSqliteContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppSqliteContext, SqliteKeyByteItem, byte[]>(context, mapper, tokenProvider );