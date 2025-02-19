namespace AuditLog.Domain.AuditLogs;

public interface IAuditLogBatchRepository
{
    Task<IReadOnlyCollection<AuditLogBatchDto>> Get(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default);

    Task<long> Count(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default);
}