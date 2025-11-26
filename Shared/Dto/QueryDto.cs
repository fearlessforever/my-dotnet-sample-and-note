using System.Text;
using static System.String;

namespace Fearlessforever.Shared.Dto;

public record QueryDto
{

    public int? Page { get; set; } = 1;
    public int? ItemsPerPage { get; set; } = 10;
    public string? SortBy { get; set; } = "Id";
    public bool? IsSortAscending { get; set; } = true;
    public string? Keyword { get; set; }
    public string? TypeList { get; set; }
    public string? DateRange { get; set; }
    public bool? ShowOnlyDeleted { get; set; } = false;
    public QueryDto() { }
    public QueryDto(int page = 1, int itemsPerPage = 10, string sortBy = "Id", bool isSortAscending = true, string? keyword = null, bool showOnlyDeleted = false)
    {
        Page = page <= 0 ? 1 : page;
        ItemsPerPage = itemsPerPage <= 0 ? 10 : itemsPerPage;
        SortBy = sortBy;
        IsSortAscending = isSortAscending;
        Keyword = keyword;
        //todo: check only superadmin role
        ShowOnlyDeleted = showOnlyDeleted;
    }

    public override string ToString()
    {
        StringBuilder sb = new();
        sb.Append(Format("{0}={1}&", nameof(Page), Page));
        sb.Append(Format("{0}={1}&", nameof(ItemsPerPage), ItemsPerPage));
        sb.Append(Format("{0}={1}&", nameof(SortBy), SortBy));
        sb.Append(Format("{0}={1}&", nameof(IsSortAscending), IsSortAscending));
        sb.Append(Format("{0}={1}&", nameof(Keyword), Keyword));
        sb.Append(Format("{0}={1}&", nameof(TypeList), TypeList));
        sb.Append(Format("{0}={1}&", nameof(DateRange), DateRange));

        return sb.ToString().TrimEnd('&');
    }
}