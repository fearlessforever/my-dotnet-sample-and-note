namespace Fearlessforever.Databases.Shared.Models;

public class Book : BaseEntitySoftDelete<int>
{
  public required string Title { get; set; }

  public string AuthorName { get; set; } = string.Empty;

  public string Description { get; set; } = string.Empty;

  public ICollection<BookCategory> BookCategories { get; set; } = [];
}