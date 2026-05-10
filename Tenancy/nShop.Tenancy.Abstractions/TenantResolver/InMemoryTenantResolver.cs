using System.Collections.Concurrent;

namespace nShop.Tenancy.Abstractions.TenantResolver;

public class InMemoryTenantResolver : ITenantResolver
{
    protected readonly ConcurrentDictionary<string, Guid> tenants = [];
    protected readonly IHostNameMatcher matcher;
    
    public InMemoryTenantResolver(IHostNameMatcher matcher)
    {
        this.tenants = [];
        this.matcher = matcher;
    }
    
    public InMemoryTenantResolver(Dictionary<string, Guid> tenants, IHostNameMatcher matcher)
    {
        this.tenants = new ConcurrentDictionary<string, Guid>(tenants);
        this.matcher = matcher;
    }
    
    public IDictionary<string, Guid> Tenants => tenants;
    
    public Task<Tenant?> FromHostName(string hostName, CancellationToken cancellationToken)
    {
        foreach (var tenant in tenants)
        {
            if (matcher.IsMatch(hostName, tenant.Key))
            {
                return Task.FromResult<Tenant?>(new Tenant() { Id = tenant.Value });
            }
        }

        return Task.FromResult<Tenant?>(null);
    }
}