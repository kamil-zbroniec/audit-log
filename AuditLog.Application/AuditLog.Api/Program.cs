using AuditLog.Api.AuditLogs;
using AuditLog.Api.AuditLogs.Entities;
using AuditLog.Api.Authentication;
using AuditLog.Api.DocumentHeaders;
using AuditLog.Api.Organizations;
using AuditLog.Domain.AuditLogs;
using AuditLog.Domain.AuditLogs.DocumentHeaders;
using AuditLog.Domain.Organizations;
using AuditLog.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSwaggerGen();

builder.Services.Configure<ApiKeyOptions>(builder.Configuration.GetSection(ApiKeyOptions.SectionName));

builder.Services.AddScoped<IAuditLogBatchRepository, AuditLogBatchRepository>();
builder.Services.AddScoped<IDocumentHeaderRepository, DocumentHeaderRepository>();
builder.Services.AddScoped<IOrganizationRepository, OrganizationRepository>();
builder.Services.AddScoped<ApiKeyAuthorizationFilter>();

builder.Services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining<Program>());

builder.Services.AddDbContext<AuditLogDbContext>((_, opts) =>
{
    opts.UseNpgsql(builder.Configuration.GetConnectionString("Database"));
});

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.Converters.Add(new EntityDetailsConverter());
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("DefaultPolicy", br =>
    {
        br
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.MapControllers();

app.UseCors("DefaultPolicy");

app.Run();