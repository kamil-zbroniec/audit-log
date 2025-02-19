namespace AuditLog.Api.Authentication;

public record ApiKeyOptions
{
    public const string SectionName = "Authentication";

    public string ApiKey { get; init; } = string.Empty;
}