namespace nShop.Catalog.DomainEvents;

public class ProductPublishedEvent : ProductEvent
{
    public DateTime TimeStamp { get; set; }
}