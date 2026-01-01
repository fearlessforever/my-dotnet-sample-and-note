using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services;

[FeatureFlag(Features.UseDatabasePostgreSql)]
public sealed class ProjectService(AppPostgreSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppPostgreSqlContext, Project, int>(context, mapper, tokenProvider );