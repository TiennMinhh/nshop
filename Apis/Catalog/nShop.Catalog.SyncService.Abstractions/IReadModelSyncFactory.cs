using nShop.Catalog.Client.Abstractions;

namespace nShop.Catalog.SyncService.Abstractions;

public interface IReadModelSyncFactory
{
    IReadModelSyncEventHandler GetSyncEventHandler(); // IReadModelSyncEventHandler handles events and updates the read model
    IReadModelSyncDataReader<T> GetReader<T>() where T : class, IDto; // IReadModelSyncDataReader reads the read model
}