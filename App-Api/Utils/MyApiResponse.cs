namespace Fearlessforever.Api.Utils;

using Microsoft.AspNetCore.Http;

public class MyApiResponse: MyApiResponse<dynamic>
{
  
  public static IResult GenerateApiResponse<T>( T? data , bool isHeaderStatus = false , int code = 200 , string message = "Ok" , string status = "success" , dynamic? errors = null )
  {
    if (isHeaderStatus && code == StatusCodes.Status204NoContent)
    {
      return Results.Json(data: null, statusCode: StatusCodes.Status204NoContent);
    }

    return Results.Json(data: new MyApiResponse<T>
    {
      Data = data,
      Code = code,
      Message = message,
      Errors = errors,
      Status = status,
    }, statusCode: isHeaderStatus == true ? code : 200);
  }
}
public class MyApiResponse<T>
{
  public string Version { get; set; }
  public string Datetime { set; get; }
  public long Timestamp { get; set; }
  public string Status { set; get; }
  public int Code { set; get; }
  public string Message { set; get; }
  public virtual T? Data { set; get; }
  public dynamic? Errors { set; get; }

  public MyApiResponse()
  {
    DateTime now = DateTime.UtcNow;
    Version = "1.0";
    Datetime = now.ToString("u");
    Timestamp = ((DateTimeOffset)now).ToUnixTimeSeconds();
    Status = "success";
    Code = 200;
    Message = "Ok";
    Data = default;
    Errors = null;
  }

  public override string ToString()
  {
    return System.Text.Json.JsonSerializer.Serialize(this);
  }

  
}