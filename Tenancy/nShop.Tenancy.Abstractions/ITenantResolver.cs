namespace nShop.Tenancy.Abstractions;

public interface ITenantResolver
{
    Task<Tenant?> FromHostName(string hostName, CancellationToken cancellationToken);
}