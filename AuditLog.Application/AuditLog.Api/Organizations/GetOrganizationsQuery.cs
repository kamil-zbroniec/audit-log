using AuditLog.Domain.Organizations;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuditLog.Api.Organizations;

public record GetOrganizationsQuery : IRequest<IReadOnlyCollection<OrganizationResponse>>;

public class GetOrganizationsQueryHandler : IRequestHandler<GetOrganizationsQuery, IReadOnlyCollection<OrganizationResponse>>
{
    private readonly IOrganizationRepository _organizationRepository;

    public GetOrganizationsQueryHandler(IOrganizationRepository organizationRepository)
    {
        _organizationRepository = organizationRepository;
    }

    public async Task<IReadOnlyCollection<OrganizationResponse>> Handle(
        GetOrganizationsQuery query,
        CancellationToken ct)
    {
        var organizations = await _organizationRepository.Get(ct);
        return organizations
            .Select(x => x.MapToResponse())
            .ToArray();
    }
}