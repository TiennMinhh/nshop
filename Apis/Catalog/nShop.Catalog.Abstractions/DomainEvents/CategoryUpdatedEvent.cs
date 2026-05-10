namespace nShop.Catalog.DomainEvents;

public class CategoryUpdatedEvent : CategoryEvent
{
    public Guid ParentId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
}