using Fearlessforever.Databases.Shared.Dto;
using FluentValidation;

namespace Fearlessforever.Api.Modules.SampleDatabase.PostgreSql;

public static partial class MinimalApiValidator
{
  // private static class ValidDate
  // {
  //   public static DateTime? Transform(DateTime? dateTime)
  //   {
  //     if (dateTime is null) return default;

  //     DateTime validDateTime = (DateTime) dateTime;

  //     if (validDateTime.Kind == DateTimeKind.Unspecified || validDateTime.Kind == DateTimeKind.Local)
  //     {
  //       // Convert to UTC before saving
  //       validDateTime = validDateTime.ToUniversalTime();
  //       // validDateTime = DateTime.SpecifyKind(validDateTime, DateTimeKind.Utc);
  //     }

  //     return validDateTime;
  //   }
  // }
  public class ProjectDtoCreateValidator : AbstractValidator<ProjectDtoCreate>
  {
    public ProjectDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }
  
  public class ProjectDtoUpdateValidator : AbstractValidator<ProjectDtoUpdate>
  {
    public ProjectDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }
  
  public class TicketDtoCreateValidator : AbstractValidator<TicketDtoCreate>
  {
    public TicketDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Owner).NotEmpty();

      When(x => x.ValidDueDate(), () =>
      {
        RuleFor(x => x.DueDate).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow).WithMessage("'Due Date' Must be In The Future");
      });
      // RuleFor(x => x.DueDate).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow ).WithMessage("'Due Date' Must be In The Future");

      RuleFor(x => x.ProjectId).NotEmpty();
    }
  }
  
  public class TicketDtoUpdateValidator : AbstractValidator<TicketDtoUpdate>
  {
    public TicketDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Owner).NotEmpty();
      RuleFor(x => x.DueDateOrigin).NotEmpty();

      When(x => x.DueDateOrigin != x.DueDate && x.ValidDueDate() , () =>
      { 
        RuleFor(x => x.DueDate).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow ).WithMessage("'Due Date' Must be In The Future");
      });
      
    }
  }

  public class TicketAndProjectDtoCreateValidator : AbstractValidator<TicketAndProjectDtoCreate>
  {
    public TicketAndProjectDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Owner).NotEmpty();

      When(x => x.ValidDueDate(), () =>
      {
        RuleFor(x => x.DueDate).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow).WithMessage("'Due Date' Must be In The Future");
      });
      
      // RuleFor(x => ValidDate.Transform(x.DueDate)).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow).WithMessage("'Due Date' Must be In The Future").OverridePropertyName("DueDate");
      
      RuleFor(x => x.Project).NotNull().WithMessage("Project Data is Required");
      RuleFor(x => x.Project).SetValidator(new ProjectDtoCreateValidator());
    }
  }
  
  public class TicketAndProjectDtoUpdateValidator : AbstractValidator<TicketAndProjectDtoUpdate>
  {
    public TicketAndProjectDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
      RuleFor(x => x.Owner).NotEmpty();

      When(x => x.ValidDueDate(), () =>
      {
        RuleFor(x => x.DueDate).NotEmpty().Must(x => x.HasValue && x.Value >= DateTime.UtcNow).WithMessage("'Due Date' Must be In The Future");
      });

      RuleFor(x => x.Project).NotNull().WithMessage("Project Data is Required");
      RuleFor(x => x.Project).SetValidator(new ProjectDtoUpdateValidator());
    }
  }
  
  public class PostgreKeyByteItemDtoCreateValidator : AbstractValidator<PostgreKeyByteItemDtoCreate>
  {
    public PostgreKeyByteItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class PostgreKeyByteItemDtoUpdateValidator : AbstractValidator<PostgreKeyByteItemDtoUpdate>
  {
    public PostgreKeyByteItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class PostgreKeyGuidItemDtoCreateValidator : AbstractValidator<PostgreKeyGuidItemDtoCreate>
  {
    public PostgreKeyGuidItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  
  public class PostgreKeyGuidItemDtoUpdateValidator : AbstractValidator<PostgreKeyGuidItemDtoUpdate>
  {
    public PostgreKeyGuidItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
}