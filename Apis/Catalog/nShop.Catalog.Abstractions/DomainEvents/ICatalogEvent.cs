namespace nShop.Catalog.DomainEvents;

public interface ICatalogEvent : IDomainEvent, INotification
{
    public DateTime Timestamp { get; set; }
}
