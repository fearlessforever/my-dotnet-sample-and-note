namespace Fearlessforever.Shared.Utils;
public static class MyConstants
{
    // public const string CurrentTimeStamp = "timezone('utc', now())";
    public const string CurrentTimeStampMsSql = "GetUtcDate()";

    public const string CurrentTimeStampSqlite = "CURRENT_TIMESTAMP";
    public const string CurrentTimeStampPostgreSql = "CURRENT_TIMESTAMP";
}