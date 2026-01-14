namespace Fearlessforever.Databases.Shared.Dto;

public record MsSqlKeyByteItemDto(
  byte[] Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct MsSqlKeyByteItemDtoCreate( string Name );
public record struct MsSqlKeyByteItemDtoUpdate( byte[] Id , string Name );

public record MsSqlKeyGuidItemDto(
  Guid Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct MsSqlKeyGuidItemDtoCreate( string Name );
public record struct MsSqlKeyGuidItemDtoUpdate( Guid Id , string Name );

public record ViewMsSqlKeyItemDto(
  string Source,
  string Id,
  string Name,
  DateTime DateCreated,
  DateTime? DateUpdated
);