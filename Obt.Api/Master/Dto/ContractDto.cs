namespace Obt.Api.Master.Dto;

public record ContractDto(
    string ContractRef,
    string SoldTo,
    string SalesUserId,
    string? SapRarRef,
    bool SapRar);
