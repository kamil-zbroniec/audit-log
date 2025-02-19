namespace AuditLog.Domain.Organizations;

public interface IOrganizationRepository
{
    Task<IReadOnlyCollection<OrganizationDto>> Get(
        CancellationToken ct = default);
}