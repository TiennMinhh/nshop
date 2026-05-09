namespace nShop.Core.SeedWork;

public interface IAggregate : IProjection
{
    Guid ProductId { get; }
    ulong Version { get; set; }
    IEnumerable<IDomainEvent> PendingEvents { get; }
    void ResetEvents();
}