using System.Collections.Generic;

namespace AuditLog.Api.Common;

public record PaginatedResponse<T>(
    int PageNumber,
    int PageSize,
    long TotalRecordCount,
    IReadOnlyCollection<T> Items);