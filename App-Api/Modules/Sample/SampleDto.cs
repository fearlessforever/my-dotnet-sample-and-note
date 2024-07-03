using System.ComponentModel.DataAnnotations;
using Fearlessforever.Shared.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Fearlessforever.Api.Modules.Sample;

public record SampleDto : SampleDtoUpdate
{
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; }

  public static SampleDto FromModel( SampleModel sampleModel )
  {
    SampleDto data = new()
    {
      Id = sampleModel.Id,
      Name = sampleModel.Name,
      Message = sampleModel.Message,
      // Status = sampleModel.Status.ToString(),
      Status = sampleModel.Status.ToString(),
      CreatedAt = sampleModel.CreatedAt,
      UpdatedAt = sampleModel.UpdatedAt,
    };

    return data;
  }
}


public record SampleDtoCreate
{
  [Required]
  public string Name { get; set; } = string.Empty;

  [Required]
  public string Message { get; set; } = string.Empty;
  
  [SampleStatusValidated]
  public string Status { get; set; } = string.Empty;

  // [Required]
  // [EnumDataType(typeof(SampleModelStatus))]
  // public SampleModelStatus Status { get; set; }
}

public record SampleDtoUpdate : SampleDtoCreate
{
  [Required]
  [FromRoute(Name = "Id")]
  public int Id { get; set; }

}

public record SampleDtoRequiredId
{
  [Required]
  [FromRoute(Name = "Id")]
  public int Id { get; set; }

}

public class SampleStatusValidated : ValidationAttribute
{
  protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
  {
    string checkStatus = $"{value}";
    string[] validStatus = ["DRAFT" , "SUBMIT"];
    
    if ( !Array.Exists(validStatus, x => x == checkStatus ))
    {
      return new ValidationResult("Invalid Status Value : DRAFT , SUBMIT");
    }

    return ValidationResult.Success;
  }
}