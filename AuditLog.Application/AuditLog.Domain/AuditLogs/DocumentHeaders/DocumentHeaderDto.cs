namespace AuditLog.Domain.AuditLogs.DocumentHeaders;

public record DocumentHeaderDto(
    Guid Id,
    string? Number);