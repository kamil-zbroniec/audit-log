using System.ComponentModel.DataAnnotations.Schema;

namespace AuditLog.Domain.AuditLogs.DocumentHeaders;

public record DocumentHeader
{
    [Column("id")]
    public Guid Id { get; init; }

    [Column("number")]
    public string? Number { get; init; }
}