namespace Fearlessforever.Databases.Shared.Dto;

public record InMemoryKeyByteItemDto(
  byte[] Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct InMemoryKeyByteItemDtoCreate( string Name );
public record struct InMemoryKeyByteItemDtoUpdate( byte[] Id , string Name );

public record InMemoryKeyGuidItemDto(
  Guid Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);

public record struct InMemoryKeyGuidItemDtoCreate( string Name );
public record struct InMemoryKeyGuidItemDtoUpdate( Guid Id , string Name );

public record InMemoryKeyIntItemDto(
  int Id,
  DateTime DateCreated,
  DateTime? DateUpdated,
  string Name
);
public record struct InMemoryKeyIntItemDtoCreate( string Name );
public record struct InMemoryKeyIntItemDtoUpdate( int Id , string Name );
