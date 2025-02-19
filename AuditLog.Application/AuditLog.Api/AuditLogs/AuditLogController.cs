using AuditLog.Api.Authentication;
using AuditLog.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.AuditLogs;

[Route("api/audit-log")]
[ServiceFilter<ApiKeyAuthorizationFilter>]
public class AuditLogController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuditLogController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<AuditLogBatchResponse>>> Get(
        [FromQuery] Guid? organizationId,
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate,
        PaginationRequest pagination, 
        CancellationToken ct = default)
    {
        var totalCount = await _mediator.Send(
            new CountAuditLogQuery(
                organizationId, 
                startDate, 
                endDate), 
            ct);
        
        var auditLogBatches = await _mediator.Send(
            new GetAuditLogQuery(
                organizationId, 
                startDate, 
                endDate,
                pagination), 
            ct); 
        
        return Ok(
            new PaginatedResponse<AuditLogBatchResponse>(
                pagination.PageNumber,
                pagination.PageSize,
                totalCount,
                auditLogBatches));
    }
}