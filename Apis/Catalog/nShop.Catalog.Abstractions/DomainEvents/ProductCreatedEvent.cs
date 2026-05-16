namespace nShop.Catalog.DomainEvents;

public class ProductCreatedEvent : ProductEvent, ITenancyEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid CategoryId { get; set; }
    public double Price { get; set; }
    public string[] Tags { get; set; } = [];
    public string Slug { get; set; } = string.Empty;
    public Guid TenantId { get; set; }
    public string[] Images { get; set; } = [];
    public Guid[] Groups { get; set; } = [];
}