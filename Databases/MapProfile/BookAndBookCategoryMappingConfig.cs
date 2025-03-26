using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models;
using Mapster;

namespace Fearlessforever.Databases.MapProfile;

public class BookAndBookCategoryMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<BookDtoUpdate, Book>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Title, src => src.Title)
        .Map(dest => dest.AuthorName, src => src.AuthorName)
        .Map(dest => dest.Description, src => src.Description)
        ;

    config.NewConfig<BookCategoryDtoUpdate, BookCategory>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Name, src => src.Name)
        ;
  }
}

