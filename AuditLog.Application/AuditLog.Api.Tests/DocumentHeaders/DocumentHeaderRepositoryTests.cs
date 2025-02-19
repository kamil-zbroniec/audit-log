using AuditLog.Api.DocumentHeaders;
using AuditLog.Domain.AuditLogs.DocumentHeaders;
using AuditLog.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AuditLog.Api.Tests.DocumentHeaders;

public class DocumentHeaderRepositoryTests
{
    private readonly AuditLogDbContext _context;
    private readonly DocumentHeaderRepository _sut;

    public DocumentHeaderRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<AuditLogDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new AuditLogDbContext(options);
        _sut = new DocumentHeaderRepository(_context);
    }

    [Fact]
    public async Task Get_WithExistingIds_ReturnsMatchingDocuments()
    {
        // Arrange
        var id1 = Guid.NewGuid();
        var id2 = Guid.NewGuid();
        
        await _context.DocumentHeaders.AddRangeAsync(
            new DocumentHeader { Id = id1, Number = "DOC-001" },
            new DocumentHeader { Id = id2, Number = "DOC-002" },
            new DocumentHeader { Id = Guid.NewGuid(), Number = "DOC-003" }
        );
        await _context.SaveChangesAsync();

        // Act
        var result = await _sut.Get([id1, id2]);

        // Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, d => d.Id == id1 && d.Number == "DOC-001");
        Assert.Contains(result, d => d.Id == id2 && d.Number == "DOC-002");
    }

    [Fact]
    public async Task Get_WithNonExistingIds_ReturnsEmptyCollection()
    {
        // Arrange
        var nonExistingIds = new[] { Guid.NewGuid(), Guid.NewGuid() };

        // Act
        var result = await _sut.Get(nonExistingIds);

        // Assert
        Assert.Empty(result);
    }
}