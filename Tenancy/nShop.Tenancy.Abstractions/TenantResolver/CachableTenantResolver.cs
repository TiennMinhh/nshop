using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace nShop.Tenancy.Abstractions.TenantResolver;

public class CachableTenantResolver : ITenantResolver
{
    private readonly ITenantResolver parent;
    private readonly IDistributedCache cache;

    public CachableTenantResolver(ITenantResolver parent, IDistributedCache cache)
    {
        this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        this.cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }
    
    public async Task<Tenant?> FromHostName(string hostName, CancellationToken cancellationToken)
    {
        var cacheValue = await cache.GetAsync(hostName);

        if (cacheValue != null)
        {
            // Value exists in the cache, deserialize it and return
            return DeserializeTenant(cacheValue);
        }

        // Value does not exist in the cache, retrieve it from the parent resolver
        Tenant? resolvedTenant = await parent.FromHostName(hostName, cancellationToken);
        
        if (resolvedTenant != null)
        {
            // Value retrieved from the parent resolver, store it in the cache
            byte[] serializedTenant = SerializeTenant(resolvedTenant);
            await cache.SetAsync(hostName, serializedTenant, token: cancellationToken);
        }

        return resolvedTenant;
    }
    
    private byte[] SerializeTenant(Tenant resolvedTenant) => JsonSerializer.SerializeToUtf8Bytes(resolvedTenant);

    private Tenant? DeserializeTenant(byte[] serializedTenant) => JsonSerializer.Deserialize<Tenant>(serializedTenant);
}