namespace Fearlessforever.Databases.Shared.Dto;

public record SqliteKeyByteItemDto(
  byte[] Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct SqliteKeyByteItemDtoCreate( string Name );

public record SqliteKeyGuidItemDto(
  Guid Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct SqliteKeyGuidItemDtoCreate( string Name );

public record SqliteKeyStringItemDto(
  string Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);
public record struct SqliteKeyStringItemDtoCreate( string Id , string Name );

public record ViewSqliteKeyItemDto(
  string Source,
  string Id,
  string Name,
  DateTime DateCreated,
  DateTime? DateUpdated
);