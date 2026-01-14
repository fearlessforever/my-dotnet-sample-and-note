using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Fearlessforever.Databases.Services;
using Fearlessforever.Databases.Services.Testing;
using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Api.Core.FluentValidation;
using Fearlessforever.Shared.Dto;
using LinqKit;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;
using Fearlessforever.Databases.Shared.Models;

namespace Fearlessforever.Api.Modules.SampleDatabase.MsSql;

public static partial class MinimalApiExtensions
{
  public static void MapMiniApiSampleDatabaseMsSql(RouteGroupBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("MsSql");

    sampleDatabases.AddMapGroupMsSqlKeyIntItemBook();
    sampleDatabases.AddMapGroupMsSqlKeyIntItemBookCategory();
    sampleDatabases.AddMapGroupPostgreSqlKeyIntItemBookCategoryAndBook();
    sampleDatabases.AddMapGroupMsSqlKeyByteItem();
    sampleDatabases.AddMapGroupMsSqlKeyGuidItem();
  }

  private static void AddMapGroupMsSqlKeyIntItemBook(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("Book").WithTags("Sample Database: (Book) MsSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] BookDtoCreate bookDtoCreate, [FromServices] BookService bookService) =>
      {
        var result = await bookService.SaveAsync<BookDto>(bookService.MapTo(bookDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BookDtoCreate>>()
      .Produces<MyApiResponse<BookDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] BookService bookService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await bookService.ListAsync<BookDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<BookDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] BookService bookService) =>
      {
        var condition = PredicateBuilder.New<Book>();
        condition.And(x => x.Id == Id);
        var result = await bookService.GetByIdAsync<BookDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BookDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] BookService bookService) =>
      {
        var result = await bookService.DeleteAsync<BookDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BookDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] BookDtoUpdate bookDtoUpdate, [FromServices] BookService bookService) =>
      {
        var condition = PredicateBuilder.New<Book>();
        condition.And(x => x.Id == Id);
        bookDtoUpdate.Id = Id;

        var result = await bookService.UpdateAsync<BookDto, BookDtoUpdate>(bookDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BookDtoUpdate>>()
      .Produces<MyApiResponse<BookDto>>();
    }
  }

  private static void AddMapGroupMsSqlKeyIntItemBookCategory(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("BookCategory").WithTags("Sample Database: (Book Category) MsSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] BookCategoryDtoCreate bookCategoryDtoCreate, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var result = await bookCategoryService.SaveAsync<BookCategoryDto>(bookCategoryService.MapTo(bookCategoryDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BookCategoryDtoCreate>>()
      .Produces<MyApiResponse<BookCategoryDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] BookCategoryService bookCategoryService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await bookCategoryService.ListAsync<BookCategoryDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<BookCategoryDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var condition = PredicateBuilder.New<BookCategory>();
        condition.And(x => x.Id == Id);
        var result = await bookCategoryService.GetByIdAsync<BookCategoryDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BookCategoryDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var result = await bookCategoryService.DeleteAsync<BookCategoryDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BookCategoryDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] BookCategoryDtoUpdate bookCategoryDtoUpdate, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var condition = PredicateBuilder.New<BookCategory>();
        condition.And(x => x.Id == Id);
        bookCategoryDtoUpdate.Id = Id;

        var result = await bookCategoryService.UpdateAsync<BookCategoryDto, BookCategoryDtoUpdate>(bookCategoryDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<BookCategoryDtoUpdate>>()
      .Produces<MyApiResponse<BookCategoryDto>>();
    }
  }

  private static void AddMapGroupPostgreSqlKeyIntItemBookCategoryAndBook(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("BookCategoryAndBook").WithTags("Sample Database: (Book & Category) MsSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] BookCategoryAndBookDtoCreate bookCategoryAndBookDtoCreate, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var result = await bookCategoryService.SaveAsync<BookCategoryAndBookDto>(bookCategoryService.MapTo(bookCategoryAndBookDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BookCategoryAndBookDtoCreate>>()
      .Produces<MyApiResponse<BookCategoryAndBookDto>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var condition = PredicateBuilder.New<BookCategory>();
        condition.And(x => x.Id == Id);
        var result = await bookCategoryService.GetByIdAsync<BookCategoryAndBookDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<BookCategoryAndBookDto>>();

      sampleDatabase.MapPut("/not-transaction/{Id}", async ([FromRoute] int Id, [FromBody] BookCategoryAndBookDtoUpdate bookCategoryAndBookDtoUpdate, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var condition = PredicateBuilder.New<BookCategory>();
        condition.And(x => x.Id == Id);
        bookCategoryAndBookDtoUpdate.Id = Id;

        // get selected entity and its sub relation
        var selectedEntity = await bookCategoryService.GetByIdAsync<BookCategory>(condition, ["Book"]);
        if (selectedEntity is null)
          return MyApiResponse.GenerateApiResponse(selectedEntity, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        // map `bookCategoryAndBookDtoUpdate` into `selectedEntity`
        selectedEntity = bookCategoryService.MapTo(bookCategoryAndBookDtoUpdate, selectedEntity);

        // update blog and author at the same time
        var result = await bookCategoryService.UpdateAsync<BookCategoryAndBookDto>(selectedEntity, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BookCategoryAndBookDtoUpdate>>()
      .Produces<MyApiResponse<BookCategoryAndBookDto>>();

      sampleDatabase.MapPut("/with-transaction/{Id}", async ([FromRoute] int Id, [FromBody] BookCategoryAndBookDtoUpdate bookCategoryAndBookDtoUpdate, [FromServices] BookCategoryService bookCategoryService) =>
      {
        var condition = PredicateBuilder.New<BookCategory>();
        condition.And(x => x.Id == Id);
        bookCategoryAndBookDtoUpdate.Id = Id;

        // update blog and author at the same time
        var result = await bookCategoryService.UpdateWithProjectTransactionAsync<BookCategoryAndBookDto, BookCategoryAndBookDtoUpdate>(bookCategoryAndBookDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<BookCategoryAndBookDtoUpdate>>()
      .Produces<MyApiResponse<BookCategoryAndBookDto>>();
    }
  }

  private static void AddMapGroupMsSqlKeyByteItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyByteItem").WithTags("Sample Database: MsSql KeyByte");
    {
      sampleDatabase.MapPost("/", async ([FromBody] MsSqlKeyByteItemDtoCreate msSqlKeyByteItemDtoCreate, [FromServices] MsSqlKeyByteItemService msSqlKeyByteItemService) =>
      {
        var result = await msSqlKeyByteItemService.SaveAsync<MsSqlKeyByteItemDto>(msSqlKeyByteItemService.MapTo(msSqlKeyByteItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<MsSqlKeyByteItemDtoCreate>>()
      .Produces<MyApiResponse<MsSqlKeyByteItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] MsSqlKeyByteItemService msSqlKeyByteItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await msSqlKeyByteItemService.ListAsync<MsSqlKeyByteItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<MsSqlKeyByteItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] string Id, [FromServices] MsSqlKeyByteItemService msSqlKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<MsSqlKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        var result = await msSqlKeyByteItemService.GetByIdAsync<MsSqlKeyByteItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<MsSqlKeyByteItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] string Id, [FromServices] MsSqlKeyByteItemService msSqlKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var result = await msSqlKeyByteItemService.DeleteAsync<MsSqlKeyByteItemDto>(validKeyId);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<MsSqlKeyByteItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] string Id, [FromBody] MsSqlKeyByteItemDtoUpdate msSqlKeyByteItemDtoUpdate, [FromServices] MsSqlKeyByteItemService msSqlKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<MsSqlKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        msSqlKeyByteItemDtoUpdate.Id = validKeyId;

        var result = await msSqlKeyByteItemService.UpdateAsync<MsSqlKeyByteItemDto, MsSqlKeyByteItemDtoUpdate>(msSqlKeyByteItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<MsSqlKeyByteItemDtoUpdate>>()
      .Produces<MyApiResponse<MsSqlKeyByteItemDto>>();
    }
  }
  
  private static void AddMapGroupMsSqlKeyGuidItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyGuidItem").WithTags("Sample Database: MsSql KeyGuid");
    {
      sampleDatabase.MapPost("/", async ([FromBody] MsSqlKeyGuidItemDtoCreate msSqlKeyGuidItemDtoCreate, [FromServices] MsSqlKeyGuidItemService msSqlKeyGuidItemService) =>
      {
        var result = await msSqlKeyGuidItemService.SaveAsync<MsSqlKeyGuidItemDto>(msSqlKeyGuidItemService.MapTo(msSqlKeyGuidItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<MsSqlKeyGuidItemDtoCreate>>()
      .Produces<MyApiResponse<MsSqlKeyGuidItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] MsSqlKeyGuidItemService msSqlKeyGuidItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await msSqlKeyGuidItemService.ListAsync<MsSqlKeyGuidItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<MsSqlKeyGuidItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] Guid Id, [FromServices] MsSqlKeyGuidItemService msSqlKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<MsSqlKeyGuidItem>();
        condition.And(x => x.Id == Id);
        var result = await msSqlKeyGuidItemService.GetByIdAsync<MsSqlKeyGuidItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<MsSqlKeyGuidItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] Guid Id, [FromServices] MsSqlKeyGuidItemService msSqlKeyGuidItemService) =>
      {
        var result = await msSqlKeyGuidItemService.DeleteAsync<MsSqlKeyGuidItemDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<MsSqlKeyGuidItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] Guid Id, [FromBody] MsSqlKeyGuidItemDtoUpdate msSqlKeyGuidItemDtoUpdate, [FromServices] MsSqlKeyGuidItemService msSqlKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<MsSqlKeyGuidItem>();
        condition.And(x => x.Id == Id);
        msSqlKeyGuidItemDtoUpdate.Id = Id;

        var result = await msSqlKeyGuidItemService.UpdateAsync<MsSqlKeyGuidItemDto, MsSqlKeyGuidItemDtoUpdate>(msSqlKeyGuidItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<MsSqlKeyGuidItemDtoUpdate>>()
      .Produces<MyApiResponse<MsSqlKeyGuidItemDto>>();
    }
  }
}

