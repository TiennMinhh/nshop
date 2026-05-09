namespace nShop.Core.IntegrationEvent;

public interface IEventBus 
{
    Task PublishAsync(IIntegrationEvent @event, CancellationToken cancellationToken = default);
    Task PublishAsync(IEnumerable<IIntegrationEvent> events, CancellationToken cancellationToken = default);
}