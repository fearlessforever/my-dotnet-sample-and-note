
using System.ComponentModel.DataAnnotations;

namespace Fearlessforever.Databases.Shared.Models;

public abstract class BaseEntity<TKey> : IBaseEntity
{
  public TKey? Id { get; set; }
  public DateTime DateCreated { get; set; }
  public DateTime? DateUpdated { get; set; }

  [MaxLength(255)]
  public string? CreatedBy { get; set; } = string.Empty;

  [MaxLength(255)]
  public string? UpdatedBy { get; set; }
}

public abstract class BaseEntitySoftDelete<TKey> : BaseEntity<TKey>, ISoftDeletable
{
  public bool? IsDeleted { get; set; }

  [MaxLength(255)]
  public string? DeletedBy { get; set; }

  public DateTime? DateDeleted { get; set; }

  public void MarkAsDeleted()
  {
    IsDeleted = true;
    DateDeleted = DateTime.UtcNow;
  }
}

public interface IBaseEntity
{
  DateTime DateCreated { get; set; }
  DateTime? DateUpdated { get; set; }
  string? CreatedBy { get; set; }
  string? UpdatedBy { get; set; }

}

public interface ISoftDeletable
{
  public bool? IsDeleted { get; set; }
  public DateTime? DateDeleted { get; set; }
  public string? DeletedBy { get; set; }
  public void MarkAsDeleted();
}