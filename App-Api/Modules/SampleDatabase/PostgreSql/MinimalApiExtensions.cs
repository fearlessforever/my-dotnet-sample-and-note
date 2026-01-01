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

namespace Fearlessforever.Api.Modules.SampleDatabase.PostgreSql;

public static partial class MinimalApiExtensions
{
  public static void MapMiniApiSampleDatabasePostgreSql(RouteGroupBuilder endpoints)
  {
    var sampleDatabases = endpoints.MapGroup("PostgreSql");

    sampleDatabases.AddMapGroupPostgreSqlKeyIntItemProject();
    sampleDatabases.AddMapGroupPostgreSqlKeyIntItemTicket();
    sampleDatabases.AddMapGroupPostgreSqlKeyIntItemTicketAndProject();
    sampleDatabases.AddMapGroupPostgreSqlKeyByteItem();
    sampleDatabases.AddMapGroupPostgreSqlKeyGuidItem();
    sampleDatabases.AddMapGroupPostgreSqlExtraItem();
  }

  private static void AddMapGroupPostgreSqlKeyIntItemProject(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("Project").WithTags("Sample Database: (Project) PostgreSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] ProjectDtoCreate projectDtoCreate, [FromServices] ProjectService projectService) =>
      {
        var result = await projectService.SaveAsync<ProjectDto>(projectService.MapTo(projectDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<ProjectDtoCreate>>()
      .Produces<MyApiResponse<ProjectDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] ProjectService projectService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await projectService.ListAsync<ProjectDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<ProjectDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] ProjectService projectService) =>
      {
        var condition = PredicateBuilder.New<Project>();
        condition.And(x => x.Id == Id);
        var result = await projectService.GetByIdAsync<ProjectDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<ProjectDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] ProjectService projectService) =>
      {
        var result = await projectService.DeleteAsync<ProjectDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<ProjectDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] ProjectDtoUpdate projectDtoUpdate, [FromServices] ProjectService projectService) =>
      {
        var condition = PredicateBuilder.New<Project>();
        condition.And(x => x.Id == Id);
        projectDtoUpdate.Id = Id;

        var result = await projectService.UpdateAsync<ProjectDto, ProjectDtoUpdate>(projectDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<ProjectDtoUpdate>>()
      .Produces<MyApiResponse<ProjectDto>>();
    }
  }

  private static void AddMapGroupPostgreSqlKeyIntItemTicket(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("Ticket").WithTags("Sample Database: (Ticket) PostgreSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] TicketDtoCreate ticketDtoCreate, [FromServices] TicketService ticketService) =>
      {
        var result = await ticketService.SaveAsync<TicketDto>(ticketService.MapTo(ticketDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<TicketDtoCreate>>()
      .Produces<MyApiResponse<TicketDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] TicketService ticketService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await ticketService.ListAsync<TicketDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<TicketDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] TicketService ticketService) =>
      {
        var condition = PredicateBuilder.New<Ticket>();
        condition.And(x => x.Id == Id);
        var result = await ticketService.GetByIdAsync<TicketDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<TicketDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] int Id, [FromServices] TicketService ticketService) =>
      {
        var result = await ticketService.DeleteAsync<TicketDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<TicketDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] int Id, [FromBody] TicketDtoUpdate ticketDtoUpdate, [FromServices] TicketService ticketService) =>
      {
        var condition = PredicateBuilder.New<Ticket>();
        condition.And(x => x.Id == Id);
        ticketDtoUpdate.Id = Id;

        var result = await ticketService.UpdateAsync<TicketDto, TicketDtoUpdate>(ticketDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<TicketDtoUpdate>>()
      .Produces<MyApiResponse<TicketDto>>();
    }
  }

  private static void AddMapGroupPostgreSqlKeyIntItemTicketAndProject(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("TicketAndProject").WithTags("Sample Database: (Ticket & Project) PostgreSql KeyInt");
    {
      sampleDatabase.MapPost("/", async ([FromBody] TicketAndProjectDtoCreate ticketAndProjectDtoCreate, [FromServices] TicketService ticketService) =>
      {
        var result = await ticketService.SaveAsync<TicketAndProjectDto>(ticketService.MapTo(ticketAndProjectDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<TicketAndProjectDtoCreate>>()
      .Produces<MyApiResponse<TicketAndProjectDto>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] int Id, [FromServices] TicketService ticketService) =>
      {
        var condition = PredicateBuilder.New<Ticket>();
        condition.And(x => x.Id == Id);
        var result = await ticketService.GetByIdAsync<TicketAndProjectDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<TicketAndProjectDto>>();

      sampleDatabase.MapPut("/not-transaction/{Id}", async ([FromRoute] int Id, [FromBody] TicketAndProjectDtoUpdate ticketAndProjectDtoUpdate, [FromServices] TicketService ticketService) =>
      {
        var condition = PredicateBuilder.New<Ticket>();
        condition.And(x => x.Id == Id);
        ticketAndProjectDtoUpdate.Id = Id;

        // get selected entity and its sub relation
        var selectedEntity = await ticketService.GetByIdAsync<Ticket>(condition, ["Project"]);
        if (selectedEntity is null)
          return MyApiResponse.GenerateApiResponse(selectedEntity, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        // map `ticketAndProjectDtoUpdate` into `selectedEntity`
        selectedEntity = ticketService.MapTo(ticketAndProjectDtoUpdate, selectedEntity);

        // update blog and author at the same time
        var result = await ticketService.UpdateAsync<TicketAndProjectDto>(selectedEntity, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<TicketAndProjectDtoUpdate>>()
      .Produces<MyApiResponse<TicketAndProjectDto>>();

      sampleDatabase.MapPut("/with-transaction/{Id}", async ([FromRoute] int Id, [FromBody] TicketAndProjectDtoUpdate ticketAndProjectDtoUpdate, [FromServices] TicketService ticketService) =>
      {
        var condition = PredicateBuilder.New<Ticket>();
        condition.And(x => x.Id == Id);
        ticketAndProjectDtoUpdate.Id = Id;

        // update blog and author at the same time
        var result = await ticketService.UpdateWithProjectTransactionAsync<TicketAndProjectDto, TicketAndProjectDtoUpdate>(ticketAndProjectDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      })
      .AddEndpointFilter<MyValidationFilter<TicketAndProjectDtoUpdate>>()
      .Produces<MyApiResponse<TicketAndProjectDto>>();
    }
  }

  private static void AddMapGroupPostgreSqlKeyByteItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyByteItem").WithTags("Sample Database: PostgreSql KeyByte");
    {
      sampleDatabase.MapPost("/", async ([FromBody] PostgreKeyByteItemDtoCreate postgreKeyByteItemDtoCreate, [FromServices] PostgreKeyByteItemService postgreKeyByteItemService) =>
      {
        var result = await postgreKeyByteItemService.SaveAsync<PostgreKeyByteItemDto>(postgreKeyByteItemService.MapTo(postgreKeyByteItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<PostgreKeyByteItemDtoCreate>>()
      .Produces<MyApiResponse<PostgreKeyByteItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] PostgreKeyByteItemService postgreKeyByteItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await postgreKeyByteItemService.ListAsync<PostgreKeyByteItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<PostgreKeyByteItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] string Id, [FromServices] PostgreKeyByteItemService postgreKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<PostgreKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        var result = await postgreKeyByteItemService.GetByIdAsync<PostgreKeyByteItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<PostgreKeyByteItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] string Id, [FromServices] PostgreKeyByteItemService postgreKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var result = await postgreKeyByteItemService.DeleteAsync<PostgreKeyByteItemDto>(validKeyId);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<PostgreKeyByteItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] string Id, [FromBody] PostgreKeyByteItemDtoUpdate postgreKeyByteItemDtoUpdate, [FromServices] PostgreKeyByteItemService postgreKeyByteItemService) =>
      {
        var validKeyId = MySecurityHelper.Base64StringToByteArray(Id);
        var condition = PredicateBuilder.New<PostgreKeyByteItem>();
        condition.And(x => x.Id == validKeyId);
        postgreKeyByteItemDtoUpdate.Id = validKeyId;

        var result = await postgreKeyByteItemService.UpdateAsync<PostgreKeyByteItemDto, PostgreKeyByteItemDtoUpdate>(postgreKeyByteItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<PostgreKeyByteItemDtoUpdate>>()
      .Produces<MyApiResponse<PostgreKeyByteItemDto>>();
    }
  }

  private static void AddMapGroupPostgreSqlKeyGuidItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("KeyGuidItem").WithTags("Sample Database: PostgreSql KeyGuid");
    {
      sampleDatabase.MapPost("/", async ([FromBody] PostgreKeyGuidItemDtoCreate postgreKeyByteItemDtoCreate, [FromServices] PostgreKeyGuidItemService postgreKeyGuidItemService) =>
      {
        var result = await postgreKeyGuidItemService.SaveAsync<PostgreKeyGuidItemDto>(postgreKeyGuidItemService.MapTo(postgreKeyByteItemDtoCreate));
        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<PostgreKeyGuidItemDtoCreate>>()
      .Produces<MyApiResponse<PostgreKeyGuidItemDto>>();

      sampleDatabase.MapGet("/", async ([FromServices] PostgreKeyGuidItemService postgreKeyGuidItemService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await postgreKeyGuidItemService.ListAsync<PostgreKeyGuidItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name"]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<PostgreKeyGuidItemDto>>>();

      sampleDatabase.MapGet("/{Id}", async ([FromRoute] Guid Id, [FromServices] PostgreKeyGuidItemService postgreKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<PostgreKeyGuidItem>();
        condition.And(x => x.Id == Id);
        var result = await postgreKeyGuidItemService.GetByIdAsync<PostgreKeyGuidItemDto>(condition);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<PostgreKeyGuidItemDto>>();

      sampleDatabase.MapDelete("/{Id}", async ([FromRoute] Guid Id, [FromServices] PostgreKeyGuidItemService postgreKeyGuidItemService) =>
      {
        var result = await postgreKeyGuidItemService.DeleteAsync<PostgreKeyGuidItemDto>(Id);

        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<PostgreKeyGuidItemDto>>();

      sampleDatabase.MapPut("/{Id}", async ([FromRoute] Guid Id, [FromBody] PostgreKeyGuidItemDtoUpdate postgreKeyGuidItemDtoUpdate, [FromServices] PostgreKeyGuidItemService postgreKeyGuidItemService) =>
      {
        var condition = PredicateBuilder.New<PostgreKeyGuidItem>();
        condition.And(x => x.Id == Id);
        postgreKeyGuidItemDtoUpdate.Id = Id;

        var result = await postgreKeyGuidItemService.UpdateAsync<PostgreKeyGuidItemDto, PostgreKeyGuidItemDtoUpdate>(postgreKeyGuidItemDtoUpdate, condition);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);

        return MyApiResponse.GenerateApiResponse(result);

      })
      .AddEndpointFilter<MyValidationFilter<PostgreKeyGuidItemDtoUpdate>>()
      .Produces<MyApiResponse<PostgreKeyGuidItemDto>>();
    }
  }
  
  private static void AddMapGroupPostgreSqlExtraItem(this RouteGroupBuilder routeGroupBuilder)
  {
    var sampleDatabase = routeGroupBuilder.MapGroup("Extra").WithTags("Sample Database: PostgreSql View,Function and Procedure");
    {
      sampleDatabase.MapPost("/", async ([FromBody] PostgreKeyGuidItemDtoCreate postgreKeyByteItemDtoCreate, [FromServices] ViewPostgreKeyService viewPostgreKeyService) =>
      {
        var result = await viewPostgreKeyService.AddByStoreProcedureAsync( MySecurityHelper.GenerateUUID() , postgreKeyByteItemDtoCreate.Name );
        return MyApiResponse.GenerateApiResponse(new { Id = result.guid , Name = result.name , Result = result.result });

      })
      .AddEndpointFilter<MyValidationFilter<PostgreKeyGuidItemDtoCreate>>()
      .Produces<MyApiResponse<int>>();

      sampleDatabase.MapGet("/", async ([FromServices] ViewPostgreKeyService viewPostgreKeyService, [AsParameters] QueryDto queryDto) =>
      {
        var (totalItems, items) = await viewPostgreKeyService.ListAsync<ViewPostgreKeyItemDto>(
          page: queryDto.Page ?? 1,
          itemsPerPage: queryDto.ItemsPerPage ?? 10,
          sortBy: queryDto.SortBy ?? "Id",
          isSortAscending: queryDto.IsSortAscending ?? false,
          keyword: queryDto.Keyword,
          searchFields: ["Name" ]
        );
        return MyApiResponse.GenerateApiResponse(new { TotalItems = totalItems, Items = items });
      }).Produces<MyApiResponse<IEnumerable<ViewPostgreKeyItemDto>>>();

      sampleDatabase.MapGet("/Function/{Id}", async ([FromRoute] Guid Id, [FromServices] ViewPostgreKeyService viewPostgreKeyService ) =>
      {
        var result = await viewPostgreKeyService.GetByIdFromFunctionAsync<PostgreKeyGuidItemDto>(Id);
        if (result is null)
          return MyApiResponse.GenerateApiResponse(result, message: $"Data Id: {Id} not found", status: "error", isHeaderStatus: true, code: 400);
      
        return MyApiResponse.GenerateApiResponse(result);
      }).Produces<MyApiResponse<PostgreKeyGuidItemDto>>();
    }
  }
}

