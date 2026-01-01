using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models;
using Mapster;

namespace Fearlessforever.Databases.MapProfile;

public class TicketAndProjectMappingConfig : IRegister
{
  public void Register(TypeAdapterConfig config)
  {
    config.NewConfig<TicketDtoUpdate, Ticket>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Owner, src => src.Owner)
        .Map(dest => dest.Description, src => src.Description)
        .Map(dest => dest.DueDate, src => src.DueDate)
        ;

    config.NewConfig<ProjectDtoUpdate, Project>()
        .IgnoreNonMapped(true)// Ignores all properties not explicitly mapped
        .Map(dest => dest.Name, src => src.Name)
        .Map(dest => dest.Description, src => src.Description)
        ;
  }
}