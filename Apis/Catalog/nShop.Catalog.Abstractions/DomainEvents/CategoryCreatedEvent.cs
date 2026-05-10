namespace nShop.Catalog.DomainEvents;

public class CategoryCreatedEvent : CategoryEvent, ITenancyEntity
{
    public Guid TenantId { get; set; }
    public Guid ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime CreationTime { get; set; }
}