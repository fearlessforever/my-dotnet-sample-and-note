using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models;
using Mapster;

namespace Fearlessforever.Databases.MapProfile;

public class AuthorMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    // .Ignore(e => e.DateCreated)
    config.NewConfig<AuthorDtoUpdate, Author>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.EmailAddress, src => src.EmailAddress)
        .Map(dest => dest.Address, src => src.Address)
        .Map(dest => dest.Name, src => src.Name)
        ;
  }
}