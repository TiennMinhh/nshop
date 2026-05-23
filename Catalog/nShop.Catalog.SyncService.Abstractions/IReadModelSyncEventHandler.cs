using nShop.Catalog.DomainEvents;

namespace nShop.Catalog.SyncService.Abstractions;

public interface IReadModelSyncEventHandler
{
    Task HandleAsync(ICatalogEvent @event);
}