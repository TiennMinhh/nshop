using nShop.Catalog.Client.Abstractions;

namespace nShop.Catalog.SyncService.Abstractions;

public interface IReadModelSyncDataReader<TEntity> where TEntity : class, IDto
{
    Task<TEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default);
}