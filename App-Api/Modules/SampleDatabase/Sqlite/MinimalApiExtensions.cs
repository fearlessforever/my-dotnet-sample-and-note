using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Fearlessforever.Databases.Services;
using Fearlessforever.Databases.Services.Testing;
using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Databases.Shared.Models;
using Fearlessforever.Api.Core.FluentValidation;
using Fearlessforever.Shared.Dto;
using LinqKit;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Modules.SampleDatabase.Sqlite;

public static partial class MinimalApiExtensions
{
  public static void MapMiniApiSampleDatabaseSqlite(RouteGroupBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("Sqlite");

    sampleDatabases.AddMapGroupSqliteKeyIntItemAuthor();
    sampleDatabases.AddMapGroupSqliteKeyIntItemBlog();
    sampleDatabases.AddMapGroupSqliteKeyIntItemBlogAndAuthor();
    sampleDatabases.AddMapGroupSqliteKeyGuidItem();
    sampleDatabases.AddMapGroupSqliteKeyByteItem();
    sampleDatabases.AddMapGroupSqliteKeyStringItem();
    sampleDatabases.AddMapGroupViewSqliteKeyItem();

    // return sampleDatabases;
  }

  private static void AddMapGroupSqliteKeyIntItemAuthor(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabaseSqlite = routeGroupBuilder.MapGroup("Author").WithTags("Sample Database: Sqlite");
    {
      sampleDatabaseSqlite.MapPost("/", async ([FromBody] AuthorDtoCreate authorDto, [FromServices] AuthorService authorService) =>
      {
        var result = await authorService.SaveAsync<AuthorDto>(authorService.MapTo<AuthorDtoCreate, Author>(authorDto));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<AuthorDtoCreate>>()
      .Produces<MyApiResponse<AuthorDto>>();

      sampleDatabaseSqlite.MapGet("/", async ([FromServices] AuthorService authorService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await authorService.ListAsync<AuthorDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name", "Address"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<AuthorDto>>>();

      sampleDatabaseSqlite.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] AuthorService authorService) =>
      {
        var condition = PredicateBuilder.New<Author>();
        condition.And(x => x.Id == Id);
        var result = await authorService.GetByIdAsync<AuthorDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<AuthorDto>>();

      sampleDatabaseSqlite.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] AuthorService authorService) =>
      {
        var result = await authorService.DeleteAsync<AuthorDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<AuthorDto>>();

      sampleDatabaseSqlite.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] AuthorDtoUpdate authorDtoUpdate, [FromServices] AuthorService authorService) =>
      {
        var condition = PredicateBuilder.New<Author>();
        condition.And(x => x.Id == Id);
        authorDtoUpdate.Id = Id;

        var result = await authorService.UpdateAsync<AuthorDto, AuthorDtoUpdate>(authorDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<AuthorDtoUpdate>>()
      .Produces<MyApiResponse<AuthorDto>>();

    }
  }

  private static void AddMapGroupSqliteKeyIntItemBlog(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabaseSqlite = routeGroupBuilder.MapGroup("Blog").WithTags("Sample Database: Sqlite");
    {
      sampleDatabaseSqlite.MapPost("/", async ([FromBody] BlogDtoCreate blogDtoCreate, [FromServices] BlogService blogService) =>
      {
        var result = await blogService.SaveAsync<BlogDto>(blogService.MapTo(blogDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BlogDtoCreate>>()
      .Produces<MyApiResponse<BlogDto>>();

      sampleDatabaseSqlite.MapGet("/", async ([FromServices] BlogService blogService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await blogService.ListAsync<BlogDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Title", "Description"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<BlogDto>>>();

      sampleDatabaseSqlite.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] BlogService blogService) =>
      {
        var condition = PredicateBuilder.New<Blog>();
        condition.And(x => x.Id == Id);
        var result = await blogService.GetByIdAsync<BlogDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BlogDto>>();

      sampleDatabaseSqlite.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] BlogService blogService) =>
      {
        var result = await blogService.DeleteAsync<BlogDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BlogDto>>();

      sampleDatabaseSqlite.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] BlogDtoUpdate blogDtoUpdate, [FromServices] BlogService blogService) =>
      {
        var condition = PredicateBuilder.New<Blog>();
        condition.And(x => x.Id == Id);
        blogDtoUpdate.Id = Id;

        var result = await blogService.UpdateAsync<BlogDto, BlogDtoUpdate>(blogDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BlogDtoUpdate>>()
      .Produces<MyApiResponse<BlogDto>>();

    }
  }

  private static void AddMapGroupSqliteKeyIntItemBlogAndAuthor(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabaseSqlite = routeGroupBuilder.MapGroup("BlogAndAuthor").WithTags("Sample Database: Sqlite ( Blog & Author Combined)");
    {
      // Required Proper Mapping for inserting Entity and its Sub Relation
      sampleDatabaseSqlite.MapPost("/", async ([FromBody] BlogAndAuthorDtoCreate blogAndAuthorDtoCreate, [FromServices] BlogService blogService) =>
      {
        var result = await blogService.SaveAsync<BlogAndAuthorDto>(blogService.MapTo(blogAndAuthorDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BlogAndAuthorDtoCreate>>()
      .Produces<MyApiResponse<BlogAndAuthorDto>>();

      sampleDatabaseSqlite.MapPut("/not-transaction/{Id}", async ([FromRoute] int Id, [FromBody] BlogAndAuthorDtoUpdate blogAndAuthorDtoUpdate, [FromServices] BlogService blogService) =>
      {
        var condition = PredicateBuilder.New<Blog>();
        condition.And(x => x.Id == Id);
        blogAndAuthorDtoUpdate.Id = Id;

        // get selected entity and its sub relation
        var selectedEntity = await blogService.GetByIdAsync<Blog>(condition, ["Author"]);
        if (selectedEntity is null)
          return MyApiResponse.GenerateApiResponse(selectedEntity, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        // map `blogAndAuthorDtoUpdate` into `selectedEntity`
        selectedEntity = blogService.MapTo(blogAndAuthorDtoUpdate, selectedEntity);

        // update blog and author at the same time
        var result = await blogService.UpdateAsync<BlogAndAuthorDto>(selectedEntity, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BlogAndAuthorDtoUpdate>>()
      .Produces<MyApiResponse<BlogAndAuthorDto>>();

      sampleDatabaseSqlite.MapPut("/with-transaction/{Id}", async ([FromRoute] int Id, [FromBody] BlogAndAuthorDtoUpdate blogAndAuthorDtoUpdate, [FromServices] BlogService blogService) =>
      {
        var condition = PredicateBuilder.New<Blog>();
        condition.And(x => x.Id == Id);
        blogAndAuthorDtoUpdate.Id = Id;

        // update blog and author at the same time
        var result = await blogService.UpdateWithAuthorTransactionAsync<BlogAndAuthorDto, BlogAndAuthorDtoUpdate>(blogAndAuthorDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BlogAndAuthorDtoUpdate>>()
      .Produces<MyApiResponse<BlogAndAuthorDto>>();

      sampleDatabaseSqlite.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] BlogService blogService) =>
      {
        var condition = PredicateBuilder.New<Blog>();
        condition.And(x => x.Id == Id);
        var result = await blogService.GetByIdAsync<BlogAndAuthorDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BlogAndAuthorDto>>();

    }
  }

  private static void AddMapGroupSqliteKeyGuidItem(this RouteGroupBuilder routeGroupBuilder)
  {
    // ==========================================
    var sampleDatabaseSqliteKeyGuidItem = routeGroupBuilder.MapGroup("SqliteKeyGuidItem").WithTags("Sample Database: SqliteKeyGuidItem");
    {
      sampleDatabaseSqliteKeyGuidItem.MapGet("/", async ([FromServices] SqliteKeyGuidItemService sqliteKeyGuidItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await sqliteKeyGuidItemService.ListAsync<SqliteKeyGuidItemDto>(
        page: queryDto.Page ?? 1,
        itemsPerPage: queryDto.ItemsPerPage ?? 10,
        sortBy: queryDto.SortBy ?? "Id",
        isSortAscending: queryDto.IsSortAscending ?? false,
        keyword: queryDto.Keyword,
        searchFields: ["Name"]
      );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<SqliteKeyGuidItemDto>>>();

      sampleDatabaseSqliteKeyGuidItem.MapPost("/", async ([FromServices] SqliteKeyGuidItemService sqliteKeyGuidItemService, [FromBody] SqliteKeyGuidItemDtoCreate sqliteKeyGuidItemDtoCreate) =>
      {
        var result = await sqliteKeyGuidItemService.SaveAsync<SqliteKeyGuidItemDto>(sqliteKeyGuidItemService.MapTo<SqliteKeyGuidItemDtoCreate, SqliteKeyGuidItem>(sqliteKeyGuidItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<SqliteKeyGuidItemDtoCreate>>()
      .Produces<MyApiResponse<SqliteKeyGuidItemDto>>();

      sampleDatabaseSqliteKeyGuidItem.MapGet("/{Id}", async ([FromRoute] string Id, [FromServices] SqliteKeyGuidItemService sqliteKeyGuidItemService) =>
      {
        var validKeyId = MySecurityHelper.StringToGuid(Id);
        var condition = PredicateBuilder.New<SqliteKeyGuidItem>();
        condition.And(x => x.Id == validKeyId);
        var result = await sqliteKeyGuidItemService.GetByIdAsync<SqliteKeyGuidItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      }).Produces<MyApiResponse<SqliteKeyGuidItemDto>>();
    }
  }

  private static void AddMapGroupSqliteKeyByteItem(this RouteGroupBuilder routeGroupBuilder)
  {
    // ==========================================
    var sampleDatabaseSqliteKeyByteItem = routeGroupBuilder.MapGroup("SqliteKeyByteItem").WithTags("Sample Database: SqliteKeyByteItem");
    {
      sampleDatabaseSqliteKeyByteItem.MapGet("/", async ([FromServices] SqliteKeyByteItemService sqliteKeyByteItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await sqliteKeyByteItemService.ListAsync<SqliteKeyByteItemDto>(
        page: queryDto.Page ?? 1,
        itemsPerPage: queryDto.ItemsPerPage ?? 10,
        sortBy: queryDto.SortBy ?? "Id",
        isSortAscending: queryDto.IsSortAscending ?? false,
        keyword: queryDto.Keyword,
        searchFields: ["Name"]
      );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<SqliteKeyByteItemDto>>>();

      sampleDatabaseSqliteKeyByteItem.MapPost("/", async ([FromServices] SqliteKeyByteItemService sqliteKeyByteItemService, [FromBody] SqliteKeyByteItemDtoCreate sqliteKeyByteItemDto) =>
      {
        var result = await sqliteKeyByteItemService.SaveAsync<SqliteKeyByteItemDto>(sqliteKeyByteItemService.MapTo<SqliteKeyByteItemDtoCreate, SqliteKeyByteItem>(sqliteKeyByteItemDto));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<SqliteKeyByteItemDtoCreate>>()
      .Produces<MyApiResponse<SqliteKeyByteItemDto>>();

      sampleDatabaseSqliteKeyByteItem.MapGet("/ById", async ([FromQuery] string Id, [FromServices] SqliteKeyByteItemService sqliteKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<SqliteKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        var result = await sqliteKeyByteItemService.GetByIdAsync<SqliteKeyByteItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      }).Produces<MyApiResponse<SqliteKeyByteItemDto>>();
    }
  }

  private static void AddMapGroupSqliteKeyStringItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabaseSqliteKeyStringItem = routeGroupBuilder.MapGroup("SqliteKeyStringItem").WithTags("Sample Database: SqliteKeyStringItem");
    {
      sampleDatabaseSqliteKeyStringItem.MapGet("/", async ([FromServices] SqliteKeyStringItemService sqliteKeyStringItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await sqliteKeyStringItemService.ListAsync<SqliteKeyStringItemDto>(
        page: queryDto.Page ?? 1,
        itemsPerPage: queryDto.ItemsPerPage ?? 10,
        sortBy: queryDto.SortBy ?? "Id",
        isSortAscending: queryDto.IsSortAscending ?? false,
        keyword: queryDto.Keyword,
        searchFields: ["Name"]
      );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<SqliteKeyStringItemDto>>>();

      sampleDatabaseSqliteKeyStringItem.MapPost("/", async ([FromServices] SqliteKeyStringItemService sqliteKeyStringItemService, [FromBody] SqliteKeyStringItemDtoCreate sqliteKeyStringItemDtoCreate) =>
      {
        var result = await sqliteKeyStringItemService.SaveAsync<SqliteKeyStringItemDto>(sqliteKeyStringItemService.MapTo(sqliteKeyStringItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<SqliteKeyStringItemDtoCreate>>()
      .Produces<MyApiResponse<SqliteKeyStringItemDto>>();

      sampleDatabaseSqliteKeyStringItem.MapGet("/{Id}", async ([FromRoute] string Id, [FromServices] SqliteKeyStringItemService sqliteKeyStringItemService) =>
      {
        var condition = PredicateBuilder.New<SqliteKeyStringItem>();
        condition.And(x => x.Id == Id);
        var result = await sqliteKeyStringItemService.GetByIdAsync<SqliteKeyStringItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      }).Produces<MyApiResponse<SqliteKeyStringItemDto>>();
    }
  }

  private static void AddMapGroupViewSqliteKeyItem(this RouteGroupBuilder routeGroupBuilder)
  { 
    var sampleDatabaseViewSqliteKeyItem = routeGroupBuilder.MapGroup("ViewSqliteKeyItem").WithTags("Sample Database: ViewSqliteKeyItem");
    { 
      sampleDatabaseViewSqliteKeyItem.MapGet("/", async ([FromServices] ViewSqliteKeyService viewSqliteKeyService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await viewSqliteKeyService.ListAsync<ViewSqliteKeyItemDto>(
        page: queryDto.Page ?? 1,
        itemsPerPage: queryDto.ItemsPerPage ?? 10,
        sortBy: queryDto.SortBy ?? "Id",
        isSortAscending: queryDto.IsSortAscending ?? false,
        keyword: queryDto.Keyword,
        searchFields: ["Name"]
      );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<ViewSqliteKeyItemDto>>>();
    }
  }
}

