using AuditLog.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuditLog.Domain.AuditLogs;

public record AuditLogEntry
{
    [Column("id")]
    public long Id { get; init; }

    [Column("organization_id")]
    public Guid OrganizationId { get; init; }
    
    [Column("user_id")]
    public Guid UserId { get; init; }

    [Column("user_email")]
    public string UserEmail { get; init; } = string.Empty;
    
    [Column("entity_type")]
    public EntityType EntityType { get; init; }
    
    [Column("type")]
    public OperationType Type { get; init; }

    [Column("old_values")]
    public string? OldValues { get; init; }
    
    [Column("new_values")]
    public string? NewValues { get; init; }

    [Column("affected_columns")]
    public string? AffectedColumns { get; init; }
    
    [Column("entity_id")]
    public Guid EntityId { get; init; }
    
    [Column("correlation_id")]
    public Guid CorrelationId { get; init; }
    
    [Column("created_date")]
    public DateTime CreatedDate { get; init; }
}