namespace VitruMerce.Bll.Dtos;

public record ProductDto(Guid Id, string Title, string Details, float Price, Guid UserId);