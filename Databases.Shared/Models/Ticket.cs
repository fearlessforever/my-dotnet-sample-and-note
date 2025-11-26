using System.ComponentModel.DataAnnotations.Schema;

namespace Fearlessforever.Databases.Shared.Models;

public class Ticket : BaseEntitySoftDelete<int>
{
  [ForeignKey("ProjectId")]
  public required int? ProjectId { get; set; }

  public Project? Project { get; set; }

  public required string Name { get; set; }

  public string Description { get; set; } = string.Empty;

  public required string Owner { get; set; }

  public DateTime? DueDate { get; set; }
}