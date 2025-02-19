using AuditLog.Domain.AuditLogs.DocumentHeaders;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.DocumentHeaders;

public class DocumentHeaderRepository : IDocumentHeaderRepository
{
    private readonly AuditLogDbContext _dbContext;

    public DocumentHeaderRepository(AuditLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<DocumentHeaderDto>> Get(
        IReadOnlyCollection<Guid> ids,
        CancellationToken ct = default) =>
        await _dbContext.DocumentHeaders
            .AsNoTracking()
            .Where(x => ids.Contains(x.Id))
            .Select(x => new DocumentHeaderDto(
                x.Id,
                x.Number))
            .ToArrayAsync(ct);
}