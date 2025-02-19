using System;
using System.Collections.Generic;

namespace AuditLog.Api.AuditLogs;

public record AuditLogBatchResponse(
    Guid OrganizationId,
    string UserEmail,
    DateTime StartDate,
    TimeSpan Duration,
    IReadOnlyCollection<AuditLogEntryResponse> Entries);