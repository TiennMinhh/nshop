namespace nShop.Tenancy.Abstractions;

public interface ITenancyEntity
{
    Guid  TenantId { get; set;  }
}