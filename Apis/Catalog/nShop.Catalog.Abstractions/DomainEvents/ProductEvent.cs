namespace nShop.Catalog.DomainEvents;

public class ProductEvent : DomainEvent, INotification
{
    public Guid ProductId { get; set; }
}