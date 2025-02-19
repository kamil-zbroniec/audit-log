using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json.Serialization;

namespace AuditLog.Api.Common;

public record PaginationRequest
{
    private const int MaxPageSize = 20;
    private const int MinPageSize = 1;
    private const int MinPageNumber = 1;

    private readonly int _pageSize = MaxPageSize;
    private readonly int _pageNumber = MinPageNumber;

    [FromQuery(Name = "pageSize")]
    [JsonPropertyName("pageSize")]
    public int PageSize
    {
        get => _pageSize;
        init => _pageSize = Math.Clamp(value, MinPageSize, MaxPageSize);
    }

    [FromQuery(Name = "pageNumber")]
    [JsonPropertyName("pageNumber")]
    public int PageNumber
    {
        get => _pageNumber;
        init => _pageNumber = Math.Max(MinPageNumber, value);
    }
}