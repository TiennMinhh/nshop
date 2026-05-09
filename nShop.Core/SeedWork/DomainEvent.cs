namespace nShop.Core.SeedWork;

public abstract class DomainEvent : IDomainEvent
{
    public DateTime Timestamp { get; set; }
}