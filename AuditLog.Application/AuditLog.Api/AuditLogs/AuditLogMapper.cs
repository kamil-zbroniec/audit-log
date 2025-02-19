using AuditLog.Abstractions;
using AuditLog.Api.AuditLogs.Entities;
using AuditLog.Domain.AuditLogs;
using AuditLog.Domain.AuditLogs.DocumentHeaders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AuditLog.Api.AuditLogs;

internal static class AuditLogMapper
{
    internal static AuditLogBatchResponse MapToResponse(this AuditLogBatchDto source,
        IReadOnlyCollection<DocumentHeaderDto> documentHeaders) =>
        new(
            source.OrganizationId,
            source.UserEmail,
            source.StartDate,
            source.Duration,
            source.Entries
                .Select(x => MapToResponse((AuditLogEntryDto)x, documentHeaders))
                .ToArray());

    private static AuditLogEntryResponse MapToResponse(this AuditLogEntryDto source, 
        IReadOnlyCollection<DocumentHeaderDto> documentHeaders)
    {
        var entityDetails = source.EntityType == EntityType.ContractHeaderEntity
            ? new ContractHeaderEntityDetails(source.Id, GetContractHeaderNumber(source.Id, documentHeaders))
            : new EntityDetails(source.Id, source.EntityType);

        return new AuditLogEntryResponse(
            entityDetails,
            source.OperationType);
    }

    private static string? GetContractHeaderNumber(
        Guid id,
        IReadOnlyCollection<DocumentHeaderDto> documentHeaders) =>
        documentHeaders
            .FirstOrDefault(x => x.Id == id)
            ?.Number;
}