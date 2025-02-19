using AuditLog.Abstractions;
using System;

namespace AuditLog.Api.AuditLogs.Entities;

public interface IEntityDetails
{
    Guid Id { get; }
    
    EntityType EntityType { get; }
}