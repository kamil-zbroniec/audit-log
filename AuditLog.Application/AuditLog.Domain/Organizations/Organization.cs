using System.ComponentModel.DataAnnotations.Schema;

namespace AuditLog.Domain;

public record Organization
{
    [Column("id")]
    public Guid Id { get; init; }

    [Column("name")]
    public string Name { get; init; } = string.Empty;
}