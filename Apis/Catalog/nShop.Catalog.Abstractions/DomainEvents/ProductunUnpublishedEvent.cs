namespace nShop.Catalog.DomainEvents;

public class ProductUnpublishedEvent : ProductEvent
{
    public DateTime TimeStamp { get; set; }
}