namespace nShop.Catalog.Aggregates;

public class Collection
{
    public Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string Name { get; set; } = string.Empty;
}