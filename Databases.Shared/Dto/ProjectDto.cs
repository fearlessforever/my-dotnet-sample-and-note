using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record ProjectDto : ProjectDtoCreate
{ 
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record ProjectDtoCreate
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  public string Description { get; set; } = string.Empty;
}

public record ProjectDtoUpdate : ProjectDtoCreate
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
}

// public record ProjectDtoAndTicketDto : ProjectDto
// {
//   public TicketDto? Ticket { get; set; }
// }

// public record ProjectDtoAndTicketDtoCreate : ProjectDtoCreate
// {
//   public TicketDtoCreate Ticket { get; set; } = new();
// }

// public record ProjectDtoAndTicketDtoUpdate: ProjectDtoUpdate
// {
//   public TicketDtoUpdate Ticket { get; set; } = new();
// }