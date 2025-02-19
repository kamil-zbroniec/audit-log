using AuditLog.Domain.AuditLogs;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.AuditLogs;

public record CountAuditLogQuery : IRequest<long>
{
    public CountAuditLogQuery(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate)
    {
        OrganizationId = organizationId;
        StartDate = startDate;
        EndDate = endDate;
    }

    public Guid? OrganizationId { get; }
    
    public DateTime? StartDate { get; }
    
    public DateTime? EndDate { get; }
}

public class CountAuditLogQueryHandler : IRequestHandler<CountAuditLogQuery, long>
{
    private readonly IAuditLogBatchRepository _auditLogBatchRepository;

    public CountAuditLogQueryHandler(IAuditLogBatchRepository auditLogBatchRepository)
    {
        _auditLogBatchRepository = auditLogBatchRepository;
    }

    public async Task<long> Handle(CountAuditLogQuery query, CancellationToken ct) =>
        await _auditLogBatchRepository.Count(
            query.OrganizationId, 
            query.StartDate,
            query.EndDate,
            ct);
}