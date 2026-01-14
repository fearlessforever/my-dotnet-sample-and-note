using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Fearlessforever.Databases.Utils;

internal sealed class GeneratorByteIdValueGenerator : Microsoft.EntityFrameworkCore.ValueGeneration.ValueGenerator<byte[]>
{
  public override bool GeneratesTemporaryValues => false;

  public override byte[] Next(EntityEntry entry)
  {
    return MySecurityHelper.GenerateUUID().ToByteArray();
  }
}