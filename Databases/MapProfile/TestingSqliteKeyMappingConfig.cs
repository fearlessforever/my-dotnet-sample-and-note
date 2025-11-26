using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;
using Mapster;

namespace Fearlessforever.Databases.MapProfile;

public class TestingSqliteKeyMappingConfig: IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<SqliteKeyByteItemDtoCreate, SqliteKeyByteItem>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Id, src => MySecurityHelper.GenerateUUID().ToByteArray())
        ;

    config.NewConfig<ViewSqliteKeyItem, ViewSqliteKeyItemDto>()
        .Map( dest => dest.Id , src => src.Source == "SqliteKeyByteItems" ? MySecurityHelper.HexStringToBase64(src.Id ?? "" ) : src.Id )
        ;
  }
}