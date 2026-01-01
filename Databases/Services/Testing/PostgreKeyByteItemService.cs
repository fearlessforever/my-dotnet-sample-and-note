using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabasePostgreSql)]
public sealed class PostgreKeyByteItemService(AppPostgreSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppPostgreSqlContext, PostgreKeyByteItem, byte[]>(context, mapper, tokenProvider );