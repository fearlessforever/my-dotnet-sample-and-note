using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models;
using Mapster;

namespace Fearlessforever.Databases.MapProfile;

public class BlogMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    // .Ignore(e => e.DateCreated)
    config.NewConfig<BlogDtoUpdate, Blog>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.Contents, src => src.Contents)
        .Map(dest => dest.Description, src => src.Description)
        ;

    config.NewConfig<BlogAndAuthorDtoUpdate, Blog>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.Contents, src => src.Contents)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.Author, src => src.Author)
        ;
  }
}