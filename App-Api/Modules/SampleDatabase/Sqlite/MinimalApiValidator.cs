using Fearlessforever.Databases.Shared.Dto;
using FluentValidation;

namespace Fearlessforever.Api.Modules.SampleDatabase.Sqlite;

public static partial class MinimalApiValidator
{
  public class AuthorCreateValidator : AbstractValidator<AuthorDtoCreate>
  {
    public AuthorCreateValidator()
    {
      RuleFor(x => x.Address).NotEmpty();

      RuleFor(x => x.EmailAddress)
        .NotEmpty().WithMessage("Email Address is required")
        .EmailAddress().WithMessage("Invalid Email Format");

      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class AuthorUpdateValidator : AbstractValidator<AuthorDtoUpdate>
  {
    public AuthorUpdateValidator()
    {
      RuleFor(x => x.Address).NotEmpty();

      RuleFor(x => x.EmailAddress)
        .NotEmpty().WithMessage("Email Address is required")
        .EmailAddress().WithMessage("Invalid Email Format");

      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class BlogCreateValidator : AbstractValidator<BlogDtoCreate>
  {
    public BlogCreateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.Contents).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }

  public class BlogUpdateValidator : AbstractValidator<BlogDtoUpdate>
  {
    public BlogUpdateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.Contents).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }

  public class BlogAndAuthorCreateValidator : AbstractValidator<BlogAndAuthorDtoCreate>
  {
    public BlogAndAuthorCreateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.Contents).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();

      RuleFor(x => x.Author).NotNull();
      RuleFor(x => x.Author).SetValidator(new AuthorCreateValidator());
    }
  }

  public class BlogAndAuthorUpdateValidator : AbstractValidator<BlogAndAuthorDtoUpdate>
  {
    public BlogAndAuthorUpdateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.Contents).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();

      RuleFor(x => x.Author).NotNull();
      RuleFor(x => x.Author).SetValidator(new AuthorUpdateValidator());
    }
  }
  
  public class SqliteKeyByteItemValidator : AbstractValidator<SqliteKeyByteItemDtoCreate>
  {
    public SqliteKeyByteItemValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class SqliteKeyGuidItemValidator : AbstractValidator<SqliteKeyGuidItemDtoCreate>
  {
    public SqliteKeyGuidItemValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class SqliteKeyStringItemValidator : AbstractValidator<SqliteKeyStringItemDtoCreate>
  {
    public SqliteKeyStringItemValidator()
    {
      RuleFor(x => x.Id).NotEmpty();
      RuleFor(x => x.Name).NotEmpty();
    }
  }
}