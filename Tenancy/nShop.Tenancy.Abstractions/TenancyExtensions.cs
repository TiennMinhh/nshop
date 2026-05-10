using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace nShop.Tenancy.Abstractions;

public static class TenancyExtensions
{
    public static Guid GetTenantId(this IConfiguration configuration)
    {
        var tid = configuration["TenantId"] ?? throw new InvalidOperationException("TenantId is missing");

        return Guid.Parse(tid);
    }

    public static void RegisterTenant(this IServiceCollection services, Guid tenantId)
    {
        services.AddSingleton<Tenant>(new Tenant() {
            Id = tenantId
        });
    }
}