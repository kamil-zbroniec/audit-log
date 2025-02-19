namespace AuditLog.Domain.AuditLogs;

public record AuditLogBatchDto(
    Guid OrganizationId,
    string UserEmail,
    DateTime StartDate,
    TimeSpan Duration,
    IReadOnlyCollection<AuditLogEntryDto> Entries);