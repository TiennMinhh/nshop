namespace nShop.Catalog.DomainEvents;

public class CategoryEvent : DomainEvent, INotification
{
    public Guid CategoryId { get; set; }
}