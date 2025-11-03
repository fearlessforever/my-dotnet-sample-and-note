using Fearlessforever.Api.Modules.Starter;
using Fearlessforever.Api.Utils;
using Fearlessforever.Shared.Utils;
using Microsoft.AspNetCore.Mvc;
using static Fearlessforever.Api.Utils.MyApiResponse;

namespace Fearlessforever.Api.Modules.Sample;

[ApiController]
[Produces("application/json", "application/xml")]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MyApiResponse))]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<SampleController> _logger;

    public SampleController(ILogger<SampleController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<SampleDto[]>))]
    public IResult Get( SampleService sampleService )
    {
        var result = sampleService.GetAll();
        _logger.LogInformation("Result {Result}", DebugHelper.ToString(result));

        return result;
    }

    [HttpGet]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<SampleDto>))]
    public IResult Get( [FromRoute] SampleDtoRequiredId sampleDtoRequiredId , SampleService sampleService )
    {
        var result = sampleService.GetById(sampleDtoRequiredId);
        _logger.LogInformation("Result {Result}", DebugHelper.ToString(result));

        return result;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<SampleDto>))]
    public IResult Create( SampleDtoCreate sampleDtoCreate , SampleService sampleService )
    {
        var result = sampleService.Add(sampleDtoCreate);
        _logger.LogInformation("Result {Result}", DebugHelper.ToString(result));

        return result;
    }

    [HttpPut]
    [Route("{Id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<SampleDto>))]
    public IResult Update( [FromRoute] int Id , [FromBody] SampleDtoUpdate sampleDtoUpdate , SampleService sampleService )
    {
        sampleDtoUpdate.Id = Id;
        _logger.LogInformation("Request {Result}", DebugHelper.ToString(sampleDtoUpdate));
        var result = sampleService.Update(sampleDtoUpdate);
        _logger.LogInformation("Result {Result}", DebugHelper.ToString(result));

        return result;
    }
    
    [HttpDelete]
    [Route("{Id}")]
    // [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MyApiResponse<object>))]
    public IResult Delete( [FromRoute]SampleDtoRequiredId sampleDtoRequiredId , SampleService sampleService  )
    {
        var result = sampleService.DeleteById(sampleDtoRequiredId);
        return result;
    }
}
