
using System.ComponentModel.DataAnnotations.Schema;

namespace Fearlessforever.Databases.Shared.Models;

public class BookCategory : BaseEntitySoftDelete<int>
{
  public required string Name { get; set; }
  
  [ForeignKey("BookId")]
  public required int? BookId { get; set; }
}