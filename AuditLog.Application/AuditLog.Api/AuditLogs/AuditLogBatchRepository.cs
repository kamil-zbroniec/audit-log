using AuditLog.Domain.AuditLogs;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.AuditLogs;

public class AuditLogBatchRepository : IAuditLogBatchRepository
{
    private readonly AuditLogDbContext _dbContext;

    public AuditLogBatchRepository(AuditLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<AuditLogBatchDto>> Get(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default) =>
        await _dbContext.AuditLog
            .AsNoTracking()
            .Where(x => !organizationId.HasValue || x.OrganizationId == organizationId)
            .GroupBy(x => x.CorrelationId)
            .Where(x => !startDate.HasValue || x.Any(item => item.CreatedDate > startDate))
            .Where(x => !endDate.HasValue || x.Any(item => item.CreatedDate < endDate))
            .OrderByDescending(x => x.First().CreatedDate)
            .Select(x => new AuditLogBatchDto(
                x.First().OrganizationId,
                x.First().UserEmail,
                x.Min(item => item.CreatedDate),
                x.Max(item => item.CreatedDate) - x.Min(item => item.CreatedDate),
                x.Select(y => new AuditLogEntryDto(y.EntityId, y.EntityType, y.Type))
                    .ToArray()))
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToArrayAsync(ct);

    public async Task<long> Count(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate,
        CancellationToken ct = default) =>
        await _dbContext.AuditLog
            .AsNoTracking()
            .Where(x => !organizationId.HasValue || x.OrganizationId == organizationId)
            .GroupBy(x => x.CorrelationId)
            .Where(x => !startDate.HasValue || x.Any(item => item.CreatedDate > startDate))
            .Where(x => !endDate.HasValue || x.Any(item => item.CreatedDate < endDate))
            .CountAsync(ct);
}