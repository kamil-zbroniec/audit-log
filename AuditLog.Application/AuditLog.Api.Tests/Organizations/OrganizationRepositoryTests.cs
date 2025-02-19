using AuditLog.Api.Organizations;
using AuditLog.Domain;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AuditLog.Api.Tests.Organizations;

public class OrganizationRepositoryTests
{
    private readonly AuditLogDbContext _context;
    private readonly OrganizationRepository _sut;

    public OrganizationRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AuditLogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AuditLogDbContext(options);
        _sut = new OrganizationRepository(_context);
    }

    [Fact]
    public async Task Get_ReturnsAllOrganizations()
    {
        // Arrange
        await _context.Organizations.AddRangeAsync(
            new Organization { Id = Guid.NewGuid(), Name = "Org 1" },
            new Organization { Id = Guid.NewGuid(), Name = "Org 2" }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.Get();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, o => o.Name == "Org 1");
        Assert.Contains(result, o => o.Name == "Org 2");
    }

    [Fact]
    public async Task Get_WithNoOrganizations_ReturnsEmptyCollection()
    {
        // Act
        var result = await _sut.Get();

        // Assert
        Assert.Empty(result);
    }
}