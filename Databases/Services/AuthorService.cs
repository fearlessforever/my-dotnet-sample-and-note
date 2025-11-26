using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services;

[FeatureFlag(Features.UseDatabaseSqlite)]
public sealed class AuthorService(AppSqliteContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppSqliteContext, Author, int>(context, mapper, tokenProvider );