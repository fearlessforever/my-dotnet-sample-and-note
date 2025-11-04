using Fearlessforever.Api.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Fearlessforever.Api.Modules.SampleHandleError;

[ApiController]
[Produces("application/json", "application/xml")]
[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MyApiResponse<object>))]
[Route("[controller]")]
public class SampleHandleErrorController : ControllerBase
{
  [HttpGet]
  public IResult Get()
  {
    throw new Exception("This is throwing An Error");
  }
}