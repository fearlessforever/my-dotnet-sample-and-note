using System.ComponentModel;
using System.Runtime.Serialization;

namespace Fearlessforever.Api.Modules.Sample;

public class SampleModel
{
  public int Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public string Message { get; set; } = string.Empty;
  public SampleModelStatus Status { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime? UpdatedAt { get; set; } = default;
}

public enum SampleModelStatus {

  [Description("Draft")]
  [EnumMember( Value = "DRAFT")]
  DRAFT,

  [Description("SUBMIT")]
  [EnumMember( Value = "SUBMIT")]
  SUBMIT
}