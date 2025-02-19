using AuditLog.Domain;
using AuditLog.Domain.AuditLogs;
using AuditLog.Domain.AuditLogs.DocumentHeaders;
using Microsoft.EntityFrameworkCore;

namespace AuditLog.Infrastructure;

public class AuditLogDbContext(DbContextOptions<AuditLogDbContext> options) : DbContext(options)
{
       public const string SchemaName = "public";
       
       public DbSet<AuditLogEntry> AuditLog { get; init; }
       
       public DbSet<DocumentHeader> DocumentHeaders { get; init; }
       
       public DbSet<Organization> Organizations { get; init; }

       protected override void OnModelCreating(ModelBuilder modelBuilder)
       {
              modelBuilder.HasDefaultSchema(SchemaName);

              modelBuilder
                     .Entity<AuditLogEntry>()
                     .ToTable("audit_log");
              
              modelBuilder
                     .Entity<DocumentHeader>()
                     .ToTable("document_header");
              
              modelBuilder
                     .Entity<Organization>()
                     .ToTable("organization");

              base.OnModelCreating(modelBuilder);
       }
}