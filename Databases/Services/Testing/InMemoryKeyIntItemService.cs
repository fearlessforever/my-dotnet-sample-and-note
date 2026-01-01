using Fearlessforever.Databases.Contexts;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Databases.Utils;
using Fearlessforever.Shared.Utils;
using MapsterMapper;

namespace Fearlessforever.Databases.Services.Testing;

[FeatureFlag(Features.UseDatabaseInMemory)]
public sealed class InMemoryKeyIntItemService(AppInMemoryContext context, IMapper mapper, CancelTokenProvider tokenProvider ) :
BaseService<AppInMemoryContext, InMemoryKeyIntItem, int>(context, mapper, tokenProvider );