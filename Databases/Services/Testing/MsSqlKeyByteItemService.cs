using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabaseMsSql)]
public sealed class MsSqlKeyByteItemService(AppMsSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppMsSqlContext, MsSqlKeyByteItem, byte[]>(context, mapper, tokenProvider );