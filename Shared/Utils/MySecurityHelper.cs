
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Fearlessforever.Shared.Utils;

public static class MySecurityHelper
{
  public static string HashPassword(string password)
  {
    var passwordBytes = Encoding.Default.GetBytes(password);
    var hashedPassword = SHA256.HashData(passwordBytes);

    return Convert.ToHexString(hashedPassword);
  }

  public static Guid GenerateUUID()
  {
    Guid myuuid = Guid.NewGuid();
    return myuuid;
  }

  public static byte[] StringToByte( string textData)
  {
    return Encoding.UTF8.GetBytes(textData);
  }
  
  public static Guid StringToGuid(string textData)
  {
    try
    {
      return Guid.Parse(textData);
    }
    catch (Exception ex)
    {
      DebugHelper.LogError($"Invalid Guid: {textData} , error message:{ex.Message}", "MySecurityHelper");
      throw;
    }
    
  }
  
  public static byte[] Base64StringToByteArray(string textData)
  {
    try
    {
      return Convert.FromBase64String(textData);
    }
    catch (Exception ex)
    {
      DebugHelper.LogError($"Invalid Base64: {textData} , error message:{ex.Message}", "MySecurityHelper");
      throw;
    }
  }
  
  public static string HexStringToBase64( string textData)
  {
    try
    {
      // convert hext string to byte[]
      var bytes = Enumerable.Range(0, textData.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(textData.Substring(x, 2), 16))
                         .ToArray();

      return Convert.ToBase64String(bytes);
    }
    catch (Exception ex)
    {
      DebugHelper.LogError($"Invalid Hex String: {textData} , error message:{ex.Message}", "MySecurityHelper");
      throw;
    }
  }
}