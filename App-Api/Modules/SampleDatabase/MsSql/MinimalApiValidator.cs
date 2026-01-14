using Fearlessforever.Databases.Shared.Dto;
using FluentValidation;

namespace Fearlessforever.Api.Modules.SampleDatabase.MsSql;

public static partial class MinimalApiValidator
{
  public class BookDtoCreateValidator : AbstractValidator<BookDtoCreate>
  {
    public BookDtoCreateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.AuthorName).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }

  public class BookDtoUpdateValidator : AbstractValidator<BookDtoUpdate>
  {
    public BookDtoUpdateValidator()
    {
      RuleFor(x => x.Title).NotEmpty();
      RuleFor(x => x.AuthorName).NotEmpty();
      RuleFor(x => x.Description).NotEmpty();
    }
  }

  public class BookCategoryDtoCreateValidator : AbstractValidator<BookCategoryDtoCreate>
  {
    public BookCategoryDtoCreateValidator()
    {
      RuleFor(x => x.BookId).NotEmpty();
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class BookCategoryDtoUpdateValidator : AbstractValidator<BookCategoryDtoUpdate>
  {
    public BookCategoryDtoUpdateValidator()
    {
      RuleFor(x => x.BookId).NotEmpty();
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class BookCategoryAndBookDtoCreateValidator : AbstractValidator<BookCategoryAndBookDtoCreate>
  {
    public BookCategoryAndBookDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();

      RuleFor(x => x.Book).NotNull().WithMessage("Book Data is Required");
      RuleFor(x => x.Book).SetValidator(new BookDtoCreateValidator());
    }
  }

  public class BookCategoryAndBookDtoUpdateValidator : AbstractValidator<BookCategoryAndBookDtoUpdate>
  {
    public BookCategoryAndBookDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();

      RuleFor(x => x.Book).NotNull().WithMessage("Book Data is Required");
      RuleFor(x => x.Book).SetValidator(new BookDtoUpdateValidator());
    }
  }

  public class MsSqlKeyByteItemDtoCreateValidator : AbstractValidator<MsSqlKeyByteItemDtoCreate>
  {
    public MsSqlKeyByteItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  public class MsSqlKeyByteItemDtoUpdateValidator : AbstractValidator<MsSqlKeyByteItemDtoUpdate>
  {
    public MsSqlKeyByteItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }

  public class MsSqlKeyGuidItemDtoCreateValidator : AbstractValidator<MsSqlKeyGuidItemDtoCreate>
  {
    public MsSqlKeyGuidItemDtoCreateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
  public class MsSqlKeyGuidItemDtoUpdateValidator : AbstractValidator<MsSqlKeyGuidItemDtoUpdate>
  {
    public MsSqlKeyGuidItemDtoUpdateValidator()
    {
      RuleFor(x => x.Name).NotEmpty();
    }
  }
}