using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Shared.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Fearlessforever.Databases;

public sealed class MySaveChangesInterceptorSingleton : MySaveChangesInterceptor { }
public class MySaveChangesInterceptor : SaveChangesInterceptor
{
  private bool IsIgnoreInterceptor { get; set; } = false;
  public void SetIgnoreInterceptorCurrentSession()
  {
    IsIgnoreInterceptor = true;
  }
  public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
    DbContextEventData eventData,
    InterceptionResult<int> result,
    CancellationToken cancellationToken = default
  )
  {
    if (eventData.Context is null || IsIgnoreInterceptor)
    {
      // reset to use custom interceptor after use
      IsIgnoreInterceptor = false;
      return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    DebugHelper.Log("My Custom Interceptor in the work", "Entity Interceptor");

    Type deleteAbleIdInteger = typeof(BaseEntitySoftDelete<int>);
    var entityEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity.GetType().BaseType == deleteAbleIdInteger);

    foreach (var entry in entityEntries)
    {
      var currentEntity = (BaseEntitySoftDelete<int>)entry.Entity;
      if (currentEntity is null) continue;

      ModifyStateValue(currentEntity, entry);
    }
    // ====================================================================
    Type deleteAbleIdGuid = typeof(BaseEntitySoftDelete<Guid>);
    entityEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity.GetType().BaseType == deleteAbleIdGuid);

    foreach (var entry in entityEntries)
    {
      var currentEntity = (BaseEntitySoftDelete<Guid>)entry.Entity;
      if (currentEntity is null) continue;

      ModifyStateValue(currentEntity, entry);
    }
    // ====================================================================
    Type deleteAbleIdString = typeof(BaseEntitySoftDelete<string>);
    entityEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity.GetType().BaseType == deleteAbleIdString);

    foreach (var entry in entityEntries)
    {
      var currentEntity = (BaseEntitySoftDelete<string>)entry.Entity;
      if (currentEntity is null) continue;

      ModifyStateValue(currentEntity, entry);
    }
    // ====================================================================
    Type deleteAbleIdByte = typeof(BaseEntitySoftDelete<byte[]>);
    entityEntries = eventData.Context.ChangeTracker.Entries().Where(e => e.Entity.GetType().BaseType == deleteAbleIdByte);

    foreach (var entry in entityEntries)
    {
      var currentEntity = (BaseEntitySoftDelete<byte[]>)entry.Entity;
      if (currentEntity is null) continue;

      ModifyStateValue(currentEntity, entry);
    }

    return base.SavingChangesAsync(eventData, result, cancellationToken);
  }

  private static void ModifyStateValue<T>(BaseEntitySoftDelete<T> currentEntity, EntityEntry entry) where T : notnull
  {
    if (entry.State == EntityState.Deleted)
    {
      entry.State = EntityState.Modified;

      currentEntity.MarkAsDeleted();
      currentEntity.DeletedBy = "[System]";
    }
    else if (entry.State == EntityState.Added)
    {
      currentEntity.DateCreated = DateTime.UtcNow;
      currentEntity.CreatedBy = "[System]";
    }
    else if (entry.State == EntityState.Modified)
    {
      currentEntity.DateUpdated = DateTime.UtcNow;
      currentEntity.UpdatedBy = "[System]";
    }
  }

  // private static void ModifyStateValue<T>(T currentEntity, EntityEntry entry) where T : BaseEntitySoftDelete<string>
  // {
  //   if (entry.State == EntityState.Deleted)
  //   {
  //     entry.State = EntityState.Modified;

  //     currentEntity.MarkAsDeleted();
  //     currentEntity.DeletedBy = "[System]";
  //   }
  //   else if (entry.State == EntityState.Added)
  //   {
  //     currentEntity.DateCreated = DateTime.UtcNow;
  //     currentEntity.CreatedBy = "[System]";
  //   }
  //   else if (entry.State == EntityState.Modified)
  //   {
  //     currentEntity.DateUpdated = DateTime.UtcNow;
  //     currentEntity.UpdatedBy = "[System]";
  //   }
  // }
}