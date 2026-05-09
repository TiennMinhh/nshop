namespace nShop.Core.SeedWork;

public interface IEventRepository
{
    Task<T?> FindAsync<T>(Guid id, ulong? version = null, CancellationToken cancellationToken = default) where T : class, IAggregate;
    Task<ulong> AddAsync<T>(Guid id, T aggregate, CancellationToken cancellationToken = default) where T : class, IAggregate;
    Task<ulong> UpdateAsync<T>(Guid id, T aggregate, ulong? expectedRevision = null, CancellationToken cancellationToken = default) where T : class, IAggregate;
    Task<ulong> DeleteAsync<T>(Guid id, T aggregate, ulong? expectedRevision = null, CancellationToken cancellationToken = default) where T : class, IAggregate;
}