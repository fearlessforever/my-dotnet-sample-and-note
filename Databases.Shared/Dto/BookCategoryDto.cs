using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record BookCategoryDto : BookCategoryDtoCreate
{
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record BookCategoryDtoBase
{ 
  [Required]
  public string Name { get; set; } = string.Empty;
}

public record BookCategoryDtoCreate: BookCategoryDtoBase
{
  [Required]
  public int BookId { get; set; }
}

public record BookCategoryDtoUpdate : BookCategoryDtoCreate
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
}

public record BookCategoryAndBookDto : BookCategoryDto
{
  public BookDto? Book { get; set; }
}

public record BookCategoryAndBookDtoCreate: BookCategoryDtoBase
{
  public BookDtoCreate Book { get; set; } = new();
}

public record BookCategoryAndBookDtoUpdate : BookCategoryDtoBase
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
  public BookDtoUpdate Book { get; set; } = new();
}