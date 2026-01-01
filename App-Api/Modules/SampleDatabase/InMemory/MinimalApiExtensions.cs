using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;
using Fearlessforever.Databases.Services.Testing;
using Fearlessforever.Databases.Shared.Dto;
using Fearlessforever.Api.Core.FluentValidation;
using Fearlessforever.Shared.Dto;
using LinqKit;
using Fearlessforever.Databases.Shared.Models.Testing;
using Fearlessforever.Shared.Utils;

namespace Fearlessforever.Api.Modules.SampleDatabase.InMemory;

public static partial class MinimalApiExtensions
{
  public static void MapMiniApiSampleDatabaseInMemory(RouteGroupBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("InMemory");

    sampleDatabases.AddMapGroupInMemoryKeyIntItem();
    sampleDatabases.AddMapGroupInMemoryKeyGuidItem();
    sampleDatabases.AddMapGroupInMemoryKeyByteItem();

    // return sampleDatabases;
  }

  private static void AddMapGroupInMemoryKeyIntItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyIntItem").WithTags("Sample Database: InMemory KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] InMemoryKeyIntItemDtoCreate inMemoryKeyIntItemDtoCreate, [FromServices] InMemoryKeyIntItemService inMemoryKeyIntItemService) =>
      {
        var result = await inMemoryKeyIntItemService.SaveAsync<InMemoryKeyIntItemDto>(inMemoryKeyIntItemService.MapTo(inMemoryKeyIntItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyIntItemDtoCreate>>()
      .Produces<MyApiResponse<InMemoryKeyIntItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] InMemoryKeyIntItemService inMemoryKeyIntItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await inMemoryKeyIntItemService.ListAsync<InMemoryKeyIntItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name" ]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<InMemoryKeyIntItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] InMemoryKeyIntItemService inMemoryKeyIntItemService) =>
      {
        var condition = PredicateBuilder.New<InMemoryKeyIntItem>();
        condition.And(x => x.Id == Id);
        var result = await inMemoryKeyIntItemService.GetByIdAsync<InMemoryKeyIntItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyIntItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] InMemoryKeyIntItemService inMemoryKeyIntItemService) =>
      {
        var result = await inMemoryKeyIntItemService.DeleteAsync<InMemoryKeyIntItemDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyIntItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] InMemoryKeyIntItemDtoUpdate inMemoryKeyIntItemDtoUpdate, [FromServices] InMemoryKeyIntItemService inMemoryKeyIntItemService) =>
      {
        var condition = PredicateBuilder.New<InMemoryKeyIntItem>();
        condition.And(x => x.Id == Id);
        inMemoryKeyIntItemDtoUpdate.Id = Id;

        var result = await inMemoryKeyIntItemService.UpdateAsync<InMemoryKeyIntItemDto, InMemoryKeyIntItemDtoUpdate>(inMemoryKeyIntItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyIntItemDtoUpdate>>()
      .Produces<MyApiResponse<InMemoryKeyIntItemDto>>();

    }
  }

  private static void AddMapGroupInMemoryKeyGuidItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyGuidItem").WithTags("Sample Database: InMemory KeyGuid");
    {
      sampleDatabase.MapPost("/", async ([FromBody] InMemoryKeyGuidItemDtoCreate inMemoryKeyGuidItemDtoCreate, [FromServices] InMemoryKeyGuidItemService inMemoryKeyGuidItemService) =>
      {
        var result = await inMemoryKeyGuidItemService.SaveAsync<InMemoryKeyGuidItemDto>(inMemoryKeyGuidItemService.MapTo(inMemoryKeyGuidItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyGuidItemDtoCreate>>()
      .Produces<MyApiResponse<InMemoryKeyGuidItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] InMemoryKeyGuidItemService inMemoryKeyGuidItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await inMemoryKeyGuidItemService.ListAsync<InMemoryKeyGuidItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name" ]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<InMemoryKeyGuidItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] Guid Id, [FromServices] InMemoryKeyGuidItemService inMemoryKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<InMemoryKeyGuidItem>();
        condition.And(x => x.Id == Id);
        var result = await inMemoryKeyGuidItemService.GetByIdAsync<InMemoryKeyGuidItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyGuidItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] Guid Id, [FromServices] InMemoryKeyGuidItemService inMemoryKeyGuidItemService) =>
      {
        var result = await inMemoryKeyGuidItemService.DeleteAsync<InMemoryKeyGuidItemDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyGuidItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] Guid Id, [FromBody] InMemoryKeyGuidItemDtoUpdate inMemoryKeyGuidItemDtoUpdate, [FromServices] InMemoryKeyGuidItemService inMemoryKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<InMemoryKeyGuidItem>();
        condition.And(x => x.Id == Id);
        inMemoryKeyGuidItemDtoUpdate.Id = Id;

        var result = await inMemoryKeyGuidItemService.UpdateAsync<InMemoryKeyGuidItemDto, InMemoryKeyGuidItemDtoUpdate>(inMemoryKeyGuidItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyGuidItemDtoUpdate>>()
      .Produces<MyApiResponse<InMemoryKeyGuidItemDto>>();

    }
  }

  private static void AddMapGroupInMemoryKeyByteItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyByteItem").WithTags("Sample Database: InMemory KeyByte");
    {
      sampleDatabase.MapPost("/", async ([FromBody] InMemoryKeyByteItemDtoCreate inMemoryKeyByteItemDtoCreate, [FromServices] InMemoryKeyByteItemService inMemoryKeyByteItemService) =>
      {
        var result = await inMemoryKeyByteItemService.SaveAsync<InMemoryKeyByteItemDto>(inMemoryKeyByteItemService.MapTo(inMemoryKeyByteItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyByteItemDtoCreate>>()
      .Produces<MyApiResponse<InMemoryKeyByteItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] InMemoryKeyByteItemService inMemoryKeyByteItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await inMemoryKeyByteItemService.ListAsync<InMemoryKeyByteItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "DateCreated",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name", "CreatedBy"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<InMemoryKeyByteItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] string Id, [FromServices] InMemoryKeyByteItemService inMemoryKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<InMemoryKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        var result = await inMemoryKeyByteItemService.GetByIdAsync<InMemoryKeyByteItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyByteItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] string Id, [FromServices] InMemoryKeyByteItemService inMemoryKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var result = await inMemoryKeyByteItemService.DeleteAsync<InMemoryKeyByteItemDto>(validKeyId);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<InMemoryKeyByteItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] string Id, [FromBody] InMemoryKeyByteItemDtoUpdate inMemoryKeyByteItemDtoUpdate, [FromServices] InMemoryKeyByteItemService inMemoryKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<InMemoryKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        inMemoryKeyByteItemDtoUpdate.Id = validKeyId;

        var result = await inMemoryKeyByteItemService.UpdateAsync<InMemoryKeyByteItemDto, InMemoryKeyByteItemDtoUpdate>(inMemoryKeyByteItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<InMemoryKeyByteItemDtoUpdate>>()
      .Produces<MyApiResponse<InMemoryKeyByteItemDto>>();

    }
  }
  
}

