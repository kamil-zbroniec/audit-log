using AuditLog.Abstractions;
using System;

namespace AuditLog.Api.AuditLogs.Entities;

public record ContractHeaderEntityDetails(
    Guid Id,
    string? Number) : 
    EntityDetails(Id, EntityType.ContractHeaderEntity);