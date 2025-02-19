using AuditLog.Abstractions;
using AuditLog.Api.AuditLogs;
using AuditLog.Domain.AuditLogs;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AuditLog.Api.Tests.AuditLogs;

public class AuditLogBatchRepositoryTests
{
    private readonly AuditLogDbContext _context;
    private readonly AuditLogBatchRepository _sut;

    public AuditLogBatchRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AuditLogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AuditLogDbContext(options);
        _sut = new AuditLogBatchRepository(_context);
    }

    [Fact]
    public async Task Get_WithFilters_ReturnsFilteredResults()
    {
        // Arrange
        var org1Id = Guid.NewGuid();
        var org2Id = Guid.NewGuid();
        var correlationId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        
        await _context.AuditLog.AddRangeAsync(
            new AuditLogEntry 
            { 
                OrganizationId = org1Id,
                CorrelationId = correlationId,
                UserEmail = "user@test.com",
                CreatedDate = now,
                EntityId = Guid.NewGuid(),
                EntityType = EntityType.FileEntity,
                Type = OperationType.Added
            }, 
            new AuditLogEntry 
            { 
                OrganizationId = org1Id,
                CorrelationId = correlationId,
                UserEmail = "user@test.com",
                CreatedDate = now.AddMinutes(1),
                EntityId = Guid.NewGuid(),
                EntityType = EntityType.Unknown,
                Type = OperationType.Added
            },
            new AuditLogEntry 
            { 
                OrganizationId = org2Id,
                CorrelationId = correlationId,
                UserEmail = "user@test.com",
                CreatedDate = now.AddMinutes(1),
                EntityId = Guid.NewGuid(),
                EntityType = EntityType.Unknown,
                Type = OperationType.Added
            });
        
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.Get(
            org1Id,
            now.AddMinutes(-1),
            now.AddMinutes(2),
            1,
            10);

        // Assert
        Assert.Single(result);
        var batch = result.First();
        Assert.Equal(2, batch.Entries.Count);
    }

    [Fact]
    public async Task Count_ReturnsCorrectBatchCount()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        
        await _context.AuditLog.AddRangeAsync(
            new AuditLogEntry 
            { 
                OrganizationId = orgId,
                CorrelationId = Guid.NewGuid(),
                CreatedDate = now
            }, 
            new AuditLogEntry 
            { 
                OrganizationId = orgId,
                CorrelationId = Guid.NewGuid(),
                CreatedDate = now
            });
        
        await _context.SaveChangesAsync();

        // Act
        var count = await _sut.Count(orgId, null, null);

        // Assert
        Assert.Equal(2, count);
    }

    [Fact]
    public async Task Get_WithPagination_ReturnsCorrectPage()
    {
        // Arrange
        var orgId = Guid.NewGuid();
        var now = DateTime.UtcNow;
        
        await _context.AuditLog.AddRangeAsync(
            new AuditLogEntry 
            { 
                OrganizationId = orgId,
                CorrelationId = Guid.NewGuid(),
                CreatedDate = now.AddMinutes(-3),
                UserEmail = "user1@test.com"
            },
            new AuditLogEntry 
            { 
                OrganizationId = orgId,
                CorrelationId = Guid.NewGuid(),
                CreatedDate = now.AddMinutes(-2),
                UserEmail = "user2@test.com"
            },
            new AuditLogEntry 
            { 
                OrganizationId = orgId,
                CorrelationId = Guid.NewGuid(),
                CreatedDate = now.AddMinutes(-1),
                UserEmail = "user3@test.com"
            });
            
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.Get(
            orgId,
            null,
            null,
            pageNumber: 2,
            pageSize: 1);

        // Assert
        Assert.Single(result);
        Assert.Equal("user2@test.com", result.First().UserEmail);
    }
}