using AuditLog.Abstractions;
using AuditLog.Api.AuditLogs.Entities;

namespace AuditLog.Api.AuditLogs;

public record AuditLogEntryResponse(
    IEntityDetails Entity,
    OperationType OperationType);