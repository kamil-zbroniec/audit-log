using AuditLog.Api.Authentication;
using AuditLog.Api.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.Organizations;

[Route("api/organization")]
[ServiceFilter<ApiKeyAuthorizationFilter>]
public class OrganizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrganizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyCollection<OrganizationResponse>>> Get(CancellationToken ct)
    {
        var organizations = await _mediator.Send(
            new GetOrganizationsQuery(),
            ct);
        return Ok(organizations);
    }
}