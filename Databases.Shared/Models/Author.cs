

namespace Fearlessforever.Databases.Shared.Models;

public class Author : BaseEntitySoftDelete<int>
{
  public required string Name { get; set; }

  public required string EmailAddress { get; set; }
  
  public required string Address { get; set; }

  public ICollection<Blog> Blogs { get; set; } = [];
}