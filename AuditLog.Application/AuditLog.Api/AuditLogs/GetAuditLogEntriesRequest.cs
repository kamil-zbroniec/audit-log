using AuditLog.Abstractions;
using AuditLog.Api.Common;
using AuditLog.Domain.AuditLogs;
using AuditLog.Domain.AuditLogs.DocumentHeaders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.AuditLogs;

public record GetAuditLogQuery : IRequest<IReadOnlyCollection<AuditLogBatchResponse>>
{
    public GetAuditLogQuery(
        Guid? organizationId,
        DateTime? startDate,
        DateTime? endDate,
        PaginationRequest pagination)
    {
        OrganizationId = organizationId;
        StartDate = startDate;
        EndDate = endDate;
        Pagination = pagination;
    }

    public Guid? OrganizationId { get; }
    
    public DateTime? StartDate { get; }
    
    public DateTime? EndDate { get; }
    
    public PaginationRequest Pagination { get; }
}

public class GetAuditLogQueryHandler : IRequestHandler<GetAuditLogQuery, IReadOnlyCollection<AuditLogBatchResponse>>
{
    private readonly IAuditLogBatchRepository _auditLogBatchRepository;
    private readonly IDocumentHeaderRepository _documentHeaderRepository;

    public GetAuditLogQueryHandler(
        IAuditLogBatchRepository auditLogBatchRepository,
        IDocumentHeaderRepository documentHeaderRepository)
    {
        _auditLogBatchRepository = auditLogBatchRepository;
        _documentHeaderRepository = documentHeaderRepository;
    }

    public async Task<IReadOnlyCollection<AuditLogBatchResponse>> Handle(GetAuditLogQuery query, CancellationToken ct)
    {
        var auditLogBatches = await _auditLogBatchRepository.Get(
            query.OrganizationId, 
            query.StartDate,
            query.EndDate,
            query.Pagination.PageNumber,
            query.Pagination.PageSize,
            ct);
        
        var contractHeaderIds = auditLogBatches
            .SelectMany(x => x.Entries)
            .Where(x => x.EntityType is EntityType.ContractHeaderEntity)
            .Select(x => x.Id)
            .ToArray();

        var contractHeaders = await _documentHeaderRepository.Get(contractHeaderIds, ct);

        return auditLogBatches
            .Select(x => x.MapToResponse(contractHeaders))
            .ToArray();
    }
}