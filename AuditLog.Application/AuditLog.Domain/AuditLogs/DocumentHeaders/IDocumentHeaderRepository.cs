namespace AuditLog.Domain.AuditLogs.DocumentHeaders;

public interface IDocumentHeaderRepository
{
    Task<IReadOnlyCollection<DocumentHeaderDto>> Get(
        IReadOnlyCollection<Guid> ids,
        CancellationToken ct = default);
}