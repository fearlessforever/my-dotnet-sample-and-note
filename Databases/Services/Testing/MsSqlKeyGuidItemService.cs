using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabaseMsSql)]
public sealed class MsSqlKeyGuidItemService(AppMsSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppMsSqlContext, MsSqlKeyGuidItem, Guid>(context, mapper, tokenProvider );