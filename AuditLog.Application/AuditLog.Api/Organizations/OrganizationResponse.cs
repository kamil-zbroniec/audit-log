using System;

namespace AuditLog.Api.Organizations;

public record OrganizationResponse(
    Guid Id,
    string Name);