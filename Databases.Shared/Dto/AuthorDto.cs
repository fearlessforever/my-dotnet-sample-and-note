using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fearlessforever.Databases.Shared.Dto;

public record AuthorDto : AuthorDtoCreate
{
  public int Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }
}

public record AuthorDtoCreate
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  [EmailAddress]
  public string EmailAddress { get; set; } = string.Empty;

  [Required]
  public string Address { get; set; } = string.Empty;
}
public record AuthorDtoUpdate:AuthorDtoCreate
{
  [Required]
  [JsonIgnore]
  public int Id { get; set; }
}

// public record struct AuthorDtoCreate(
//   [Required]
//   string Name ,

//   [Required]
//   [EmailAddress]
//   string EmailAddress,

//   [Required]
//   string Address
// );