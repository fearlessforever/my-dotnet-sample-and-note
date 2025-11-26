
using System.Linq.Expressions;
using Fearlessforever.Databases.Shared.Models;
using Mapster;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Fearlessforever.Shared.Utils;
using MapsterMapper;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Fearlessforever.Databases.Services;

public interface IBaseService<TModel, TKey> where TModel : BaseEntity<TKey>, ISoftDeletable
{
  Task<(int totalItems, ICollection<T>)> ListAsync<T>(
      Expression<Func<TModel, bool>>? filter = null,
      int page = 1,
      int itemsPerPage = 10,
      string sortBy = "Id",
      bool isSortAscending = true,
      string? keyword = null,
      string[]? searchFields = null,
      bool showDeleted = false
  );

  Task<T?> GetByIdAsync<T>(Expression<Func<TModel, bool>> predicateToGetId , params string[]? includes);
  Task<T> SaveAsync<T>(TModel entity);
  Task<T?> UpdateAsync<T, TResource>(TResource toBeEntity, Expression<Func<TModel, bool>> where );
  Task<T?> UpdateAsync<T>(TModel updatedEntity, Expression<Func<TModel, bool>> where );
  Task<T?> DeleteAsync<T>(TKey id);
  // Task<Response<T>> HardDelete(TKey id);
  // Task<Response<T>> ExecuteInTransactionAsync<T>(Func<Task<Response<T>>> action);
  // Task<Response<T>> RestoreSoftDeleteAsync(TKey id, bool includeChildren = false);

  TDestination MapTo<TSource, TDestination>(TSource source , TDestination destination);
  TDestination MapTo<TSource, TDestination>(TSource source);
  TModel MapTo<TSource>(TSource source);
  void CustomSaveChangesBehavior();

}

public abstract class BaseService<TContext, TModel, TKey>(TContext context, IMapper mapper, CancelTokenProvider cancelTokenProvider /* , IServiceProvider serviceProvider */ ) : IBaseService<TModel, TKey>
where TContext : DbContext
where TModel : BaseEntity<TKey>, ISoftDeletable
{
  public TDestination MapTo<TSource, TDestination>(TSource source , TDestination destination)
  {
    return mapper.Map(source, destination);
  }

  public TDestination MapTo<TSource, TDestination>(TSource source)
  {
    return source.Adapt<TDestination>();
  }

  public TModel MapTo<TSource>(TSource source)
  {
    return source.Adapt<TModel>();
  }

  /// <summary>
  /// Only disabled once , but after sacing changes without custom interceptor it will be reset to use custom interceptor again
  /// </summary>
  public virtual void CustomSaveChangesBehavior()
  {
    // MySaveChangesInterceptor saveChangesInterceptor = serviceProvider.GetRequiredService<MySaveChangesInterceptor>();
    // saveChangesInterceptor.SetIgnoreInterceptorCurrentSession();
  }

  public async Task<T?> DeleteAsync<T>(TKey id)
  {
    // throw new NotImplementedException();
    var entity = await context.Set<TModel>().FindAsync(id, cancelTokenProvider.Token);

    if (entity is null) return default;

    context.Remove(entity);
    await context.SaveChangesAsync(cancelTokenProvider.Token);
    T result = entity.Adapt<T>();
    return result;
  }

  public async Task<T?> GetByIdAsync<T>(Expression<Func<TModel, bool>> predicateToGetId , params string[]? includes)
  {
    IQueryable<TModel> query = EntityFrameworkQueryableExtensions.AsNoTracking(context.Set<TModel>());
    query = query.Where(predicateToGetId);

    Type typeReturn = typeof(T);
    Type typeModel = typeof(TModel);

    if (includes != null && includes.Length > 0 && typeReturn.FullName == typeModel.FullName)
    {
      query = ApplyIncludes(query, includes);
    }

    return await query.ProjectToType<T>().FirstOrDefaultAsync(cancelTokenProvider.Token);
  }

  public async Task<(int totalItems, ICollection<T>)> ListAsync<T>(
    Expression<Func<TModel, bool>>? filter = null,
    int page = 1,
    int itemsPerPage = 10,
    string sortBy = "Id",
    bool isSortAscending = true,
    string? keyword = null,
    string[]? searchFields = null,
    bool showOnlyDeleted = false
  )
  {
    IQueryable<TModel> query = context.Set<TModel>().AsNoTracking();

    if (filter != null)
    {
      query = query.Where(filter);
    }

    if (!string.IsNullOrEmpty(keyword) && searchFields != null && searchFields.Length > 0)
    {
      var keywordLower = keyword.ToLower();
      var keywordPredicate = PredicateBuilder.New<TModel>(false);

      foreach (var field in searchFields)
      {
        keywordPredicate = keywordPredicate.Or(e =>
          EF.Property<string>(e, field).ToLower().Contains(keywordLower)
        );
      }
      query = query.Where(keywordPredicate);
    }

    if (!string.IsNullOrEmpty(sortBy))
    {
      Type entityType = typeof(TModel);
      // Use GetProperty to check if the property exists
      PropertyInfo? property = entityType.GetProperty(sortBy);
      sortBy = property is null ? "Id" : sortBy;

      query = isSortAscending
        ? query.OrderBy(e => EF.Property<object>(e, sortBy))
        : query.OrderByDescending(e => EF.Property<object>(e, sortBy));
    }
    int totalItems = await query.CountAsync(cancelTokenProvider.Token);
    query = query
        .Skip((page - 1) * itemsPerPage)
        .Take(itemsPerPage);

    return (totalItems, await query.ProjectToType<T>().ToListAsync(cancelTokenProvider.Token));
  }

  public async Task<T> SaveAsync<T>(TModel entity)
  {
    await context.AddAsync(entity, cancelTokenProvider.Token);
    await context.SaveChangesAsync(cancelTokenProvider.Token);

    return entity.Adapt<T>();
  }

  public async Task<T?> UpdateAsync<T, TResource>(TResource toBeEntity, Expression<Func<TModel, bool>> where)
  {
    var query = context.Set<TModel>().Where(where);
    
    var existingEntity = await query.FirstOrDefaultAsync(cancelTokenProvider.Token);
    if (toBeEntity == null || existingEntity == null) return default;

    mapper.Map(toBeEntity, existingEntity);

    context.Update(existingEntity);
    // context.Entry(existingEntity).State = EntityState.Modified;

    await context.SaveChangesAsync(cancelTokenProvider.Token);

    return existingEntity.Adapt<T>();
  }
  public async Task<T?> UpdateAsync<T>(TModel updatedEntity, Expression<Func<TModel, bool>> where )
  {
    // making sure that updatedEntity is already in database . not something new
    var query = context.Set<TModel>().Where(where);
    var existingEntity = await query.FirstOrDefaultAsync(cancelTokenProvider.Token);
    if (updatedEntity == null || existingEntity == null) return default;

    // mapping `updatedEntity` data into `existingEntity`
    mapper.Map(updatedEntity, existingEntity);
    context.Update(existingEntity);
    await context.SaveChangesAsync(cancelTokenProvider.Token);

    return existingEntity.Adapt<T>();
  }
  
  private static IQueryable<TModel> ApplyIncludes(IQueryable<TModel> query, params string[] includes)
  {
    foreach (var include in includes)
    {
        var includeParts = include.Split('.');
        query = ApplyNestedIncludes(query, includeParts, 0);
    }
    return query;
  }

  private static IQueryable<TModel> ApplyNestedIncludes(IQueryable<TModel> query, string[] includeParts, int index)
  {
    if (index == includeParts.Length)
        return query;

    var includePart = includeParts[index];

    if (index == 0)
    {
        query = query.Include(includePart);
    }
    else
    {
        var navigationPropertyPath = string.Join(".", includeParts.Take(index + 1));
        query = query.Include(navigationPropertyPath);
    }

    return ApplyNestedIncludes(query, includeParts, index + 1);
  }
}