using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record TicketDto : TicketDtoCreate
{ 
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record TicketDtoBase
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  public string Description { get; set; } = string.Empty;

  [Required]
  public string Owner { get; set; } = string.Empty;

  [Required]
  public DateTime? DueDate { get; set; }

  /// <summary>
  /// This method is to make sure the date is specified with timezone
  /// </summary>
  /// <returns></returns>
  public bool ValidDueDate()
  {
    if (DueDate is null) return true;

    DateTime validDateTime = (DateTime)DueDate;

    if (validDateTime.Kind == DateTimeKind.Unspecified || validDateTime.Kind == DateTimeKind.Local)
    {
      // Convert to UTC before saving
      // validDateTime = validDateTime.ToUniversalTime();
      validDateTime = DateTime.SpecifyKind(validDateTime, DateTimeKind.Utc);
      DueDate = validDateTime;
    }
    return true;
  }
}

public record TicketDtoCreate : TicketDtoBase
{
  [Required]
  public int ProjectId { get; set; }
}

public record TicketDtoUpdate : TicketDtoBase
{
  [JsonIgnore]
  public int Id { get; set; }
  public DateTime? DueDateOrigin { get; set; }
}

public record TicketAndProjectDto : TicketDto
{
  public ProjectDto? Project { get; set; }
}

public record TicketAndProjectDtoCreate : TicketDtoBase
{
  public ProjectDtoCreate Project { get; set; } = new();
}

public record TicketAndProjectDtoUpdate : TicketDtoUpdate
{
  public ProjectDtoUpdate Project { get; set; } = new();
}