
using System.ComponentModel.DataAnnotations.Schema;

namespace Fearlessforever.Databases.Shared.Models;

public class Blog : BaseEntitySoftDelete<int>
{
  public required string Title { get; set; }

  public string Contents { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  [ForeignKey("AuthorId")]
  public required int? AuthorId { get; set; }

  public Author? Author { get; set; }
}