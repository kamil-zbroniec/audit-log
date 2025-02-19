using AuditLog.Domain.Organizations;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.Organizations;

public class OrganizationRepository : IOrganizationRepository
{
    private readonly AuditLogDbContext _dbContext;

    public OrganizationRepository(AuditLogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<OrganizationDto>> Get(
        CancellationToken ct = default) => 
        await _dbContext.Organizations
            .AsNoTracking()
            .Select(x => new OrganizationDto(
                x.Id,
                x.Name))
            .ToArrayAsync(ct);
}