using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record BookDto : BookDtoCreate
{ 
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record BookDtoBase
{
  [Required]
  public string Title { get; set; } = string.Empty;

  [Required]
  public string AuthorName { get; set; } = string.Empty;

  [Required]
  public string Description { get; set; } = string.Empty;
}

public record BookDtoCreate : BookDtoBase { }

public record BookDtoUpdate : BookDtoBase
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
}