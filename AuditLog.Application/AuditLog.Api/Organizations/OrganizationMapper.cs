using AuditLog.Domain.Organizations;

namespace AuditLog.Api.Organizations;

internal static class OrganizationMapper
{
    internal static OrganizationResponse MapToResponse(this OrganizationDto source) => 
        new(source.Id, source.Name);
}