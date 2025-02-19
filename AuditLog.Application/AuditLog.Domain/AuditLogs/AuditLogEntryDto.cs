using AuditLog.Abstractions;

namespace AuditLog.Domain.AuditLogs;

public record AuditLogEntryDto(
    Guid Id, 
    EntityType EntityType, 
    OperationType OperationType);