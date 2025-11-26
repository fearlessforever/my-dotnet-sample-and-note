using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record BlogDto : BlogDtoCreate
{ 
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record BlogDtoBase
{
  [Required]
  public string Title { get; set; } = string.Empty;

  [Required]
  public string Contents { get; set; } = string.Empty;

  [Required]
  public string Description { get; set; } = string.Empty;
}

public record BlogDtoCreate:BlogDtoBase
{

  [Required]
  public int? AuthorId { get; set; }
}

public record BlogDtoUpdate : BlogDtoBase
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
}

public record BlogAndAuthorDto : BlogDto
{
  public AuthorDto? Author { get; set; }
}

public record BlogAndAuthorDtoCreate : BlogDtoBase
{
  public AuthorDtoCreate Author { get; set; } = new();
}

public record BlogAndAuthorDtoUpdate: BlogDtoUpdate
{
  public AuthorDtoUpdate Author { get; set; } = new();
}