namespace Fearlessforever.Databases.Shared.Dto;

public record PostgreKeyByteItemDto(
  byte[] Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct PostgreKeyByteItemDtoCreate( string Name );
public record struct PostgreKeyByteItemDtoUpdate( byte[] Id , string Name );

public record PostgreKeyGuidItemDto(
  Guid Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct PostgreKeyGuidItemDtoCreate( string Name );
public record struct PostgreKeyGuidItemDtoUpdate( Guid Id , string Name );

public record ViewPostgreKeyItemDto(
  string Source,
  string Id,
  string Name,
  DateTime DateCreated,
  DateTime? DateUpdated
);