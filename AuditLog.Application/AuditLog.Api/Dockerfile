FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

ARG APP_UID=1001
WORKDIR /app

RUN adduser --disabled-password --gecos '' --uid $APP_UID appuser && \
    chown -R appuser:appuser /app

USER appuser
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["AuditLog.Application/AuditLog.Abstractions/", "AuditLog.Application/AuditLog.Abstractions/"]
COPY ["AuditLog.Application/AuditLog.Api/", "AuditLog.Application/AuditLog.Api/"]
COPY ["AuditLog.Application/AuditLog.Domain/", "AuditLog.Application/AuditLog.Domain/"]
COPY ["AuditLog.Application/AuditLog.Infrastructure/", "AuditLog.Application/AuditLog.Infrastructure/"]
RUN dotnet restore "AuditLog.Application/AuditLog.Api/AuditLog.Api.csproj"

COPY . .
WORKDIR "/src/AuditLog.Application/AuditLog.Api"
RUN dotnet build "AuditLog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "AuditLog.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

COPY --chown=appuser:appuser --from=publish /app/publish .

ENTRYPOINT ["dotnet", "AuditLog.Api.dll"]