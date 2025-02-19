using AuditLog.Abstractions;
using System;

namespace AuditLog.Api.AuditLogs.Entities;

public record EntityDetails(
    Guid Id, 
    EntityType EntityType) : IEntityDetails;