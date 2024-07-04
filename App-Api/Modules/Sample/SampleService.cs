using static Fearlessforever.Api.Utils.MyApiResponse;
namespace Fearlessforever.Api.Modules.Sample;

public class SampleService
{
  private static readonly ICollection<SampleModel> data = [];
  private static int IdStart = 0;

  public IResult Add(SampleDtoCreate sampleDtoCreate)
  {
    SampleModel newData = new()
    {
      Id = ++IdStart,
      Name = sampleDtoCreate.Name,
      Message = sampleDtoCreate.Message,
      // Status = sampleDtoCreate.Status,
      Status = Enum.Parse<SampleModelStatus>(sampleDtoCreate.Status),
      CreatedAt = DateTime.UtcNow
    };

    data.Add(newData);

    return GenerateApiResponse(data: SampleDto.FromModel(newData));
  }

  public IResult Update(SampleDtoUpdate sampleDtoUpdate)
  {
    var selected = data.Where(x => x.Id == sampleDtoUpdate.Id).FirstOrDefault();
    if (selected == null)
      return GenerateApiResponse<object?>(data: null, isHeaderStatus: true, code: StatusCodes.Status404NotFound , status: "error" , message:$"Data id: ({sampleDtoUpdate.Id}) not Found");

    selected.Name = sampleDtoUpdate.Name;
    selected.Message = sampleDtoUpdate.Message;
    selected.Status = Enum.Parse<SampleModelStatus>(sampleDtoUpdate.Status);
    // selected.Status = (SampleModelStatus)Enum.Parse(typeof(SampleModelStatus), sampleDtoUpdate.Status);
    // selected.Status = sampleDtoUpdate.Status;
    selected.UpdatedAt = DateTime.UtcNow;

    return GenerateApiResponse(data: SampleDto.FromModel(selected));
  }

  public IResult GetAll()
  {
    SampleDto[] results = data.Select(x => SampleDto.FromModel(x)).ToArray() ?? [];
    return GenerateApiResponse(data: results);
  }

  public IResult GetById(SampleDtoRequiredId sampleDtoRequiredId)
  {
    SampleDto? result = data.Where(x => x.Id == sampleDtoRequiredId.Id).Select(x => SampleDto.FromModel(x)).FirstOrDefault();
    if (result == null)
      return GenerateApiResponse<object?>(data: null, isHeaderStatus: true, code: StatusCodes.Status404NotFound , status: "error" , message:$"Data id: ({sampleDtoRequiredId.Id}) not Found");

    return GenerateApiResponse(data: result );
  }

  public IResult DeleteById( SampleDtoRequiredId sampleDtoRequiredId )
  { 
    SampleModel? result = data.Where(x => x.Id == sampleDtoRequiredId.Id).FirstOrDefault();
    if (result == null)
      return GenerateApiResponse<object>(data: null, isHeaderStatus: true, code: StatusCodes.Status404NotFound , status: "error" , message:$"Data id: ({sampleDtoRequiredId.Id}) not Found");

    data.Remove(result);
    return GenerateApiResponse<object>(data: null);
  }
}