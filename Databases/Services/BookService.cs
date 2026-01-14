using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services;

[FeatureFlag(Features.UseDatabaseMsSql)]
public sealed class BookService(AppMsSqlContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppMsSqlContext, Book, int>(context, mapper, tokenProvider );