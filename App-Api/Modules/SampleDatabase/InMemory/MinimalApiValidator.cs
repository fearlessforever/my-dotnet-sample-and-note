using Fearlessforever.Databases.Shared.Dto;
using FluentValidation;

namespace Fearlessforever.Api.Modules.SampleDatabase.InMemory;

public static partial class MinimalApiValidator
{
  public class InMemoryKeyIntItemDtoCreateValidator : AbstractValidator<InMemoryKeyIntItemDtoCreate>
  {
    public InMemoryKeyIntItemDtoCreateValidator()
    {
       RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class InMemoryKeyIntItemDtoUpdateValidator : AbstractValidator<InMemoryKeyIntItemDtoUpdate>
  {
    public InMemoryKeyIntItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class InMemoryKeyGuidItemDtoCreateValidator : AbstractValidator<InMemoryKeyGuidItemDtoCreate>
  {
    public InMemoryKeyGuidItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class InMemoryKeyGuidItemDtoUpdateValidator : AbstractValidator<InMemoryKeyGuidItemDtoUpdate>
  {
    public InMemoryKeyGuidItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class InMemoryKeyByteItemDtoCreateValidator : AbstractValidator<InMemoryKeyByteItemDtoCreate>
  {
    public InMemoryKeyByteItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class InMemoryKeyByteItemDtoUpdateValidator : AbstractValidator<InMemoryKeyByteItemDtoUpdate>
  {
    public InMemoryKeyByteItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
}