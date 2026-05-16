using System.Text.Json;
using EventStore.Client;
using nShop.Core;
using nShop.Core.SeedWork;

namespace nShop.Infrastructure.EventStore.EventStoreDB;

public class EventStoreDBRepository(EventStoreClient client, IStreamNameMapper? streamNameMapper = default, IEventTypeNameMapper? eventTypeNameMapper = default) : IEventRepository
{
    private readonly EventStoreClient _client = client;
    private readonly IStreamNameMapper _streamNameMapper = streamNameMapper ?? DefaultStreamNameMapper.Instance;
    private readonly IEventTypeNameMapper _eventTypeNameMapper = eventTypeNameMapper ?? DefaultEventTypeNameMapper.Instance;
    
    public EventStoreDBRepository(string connectionString, IStreamNameMapper? streamNameMapper = default, IEventTypeNameMapper? eventTypeNameMapper = default): 
        this(new EventStoreClient(EventStoreClientSettings.Create(connectionString)), streamNameMapper, eventTypeNameMapper)
    {
    }
    
    public async Task<T?> FindAsync<T>(Guid id, ulong? version = null, CancellationToken cancellationToken = default) where T : class, IAggregate
    {
        var readResult = _client.ReadStreamAsync(
            Direction.Forwards,
            _streamNameMapper.Map(id),
            version ?? StreamPosition.Start,
            cancellationToken: cancellationToken
        );

        if (await readResult.ReadState == ReadState.StreamNotFound)
        {
            return null;
        }

        var aggregate = Activator.CreateInstance<T>();

        await readResult.AggregateAsync(aggregate, (a, e) => {
            var eventData = CreateEvent(e);
            if (eventData != null)
            {
                a.Project(eventData);
            }

            a.Version = e.Event.EventNumber.ToUInt64();
            return a;
        }, cancellationToken: cancellationToken);

        //await foreach (var resolvedEvent in readResult)
        //{
        //    var eventData = CreateEvent(resolvedEvent);

        //    if (eventData is null)
        //    {
        //        continue;
        //    }
        //    aggregate.Project(eventData);
        //}

        return aggregate;
    }
    
    private object? CreateEvent(ResolvedEvent resolvedEvent)
    {
        var t = _eventTypeNameMapper.ToType(resolvedEvent.Event.EventType);

        if (t is null)
        {
            return null;
        }

        return JsonSerializer.Deserialize(resolvedEvent.Event.Data.Span, t);
    }

    public async Task<ulong> AddAsync<T>(Guid id, T aggregate, CancellationToken cancellationToken = default) where T : class, IAggregate
    {
        var result = await _client.AppendToStreamAsync(
            _streamNameMapper.Map(id),
            StreamState.NoStream,
            aggregate.PendingEvents.Select(e => new EventData(
                Uuid.NewUuid(),
                _eventTypeNameMapper.ToName(e.GetType()),
                JsonSerializer.SerializeToUtf8Bytes(e, e.GetType())
            )),
            cancellationToken: cancellationToken
        );

        aggregate.Version = result.NextExpectedStreamRevision.ToUInt64();

        return aggregate.Version;
    }

    public async Task<ulong> UpdateAsync<T>(Guid id, T aggregate, ulong? version = null,
        CancellationToken cancellationToken = default) where T : class, IAggregate
    {
        var events = aggregate.PendingEvents.Select(e => new EventData(
            Uuid.NewUuid(),
            _eventTypeNameMapper.ToName(e.GetType()),
            JsonSerializer.SerializeToUtf8Bytes(e, e.GetType())
        ));

        var result = await _client.AppendToStreamAsync(
            _streamNameMapper.Map(id),
            version ?? aggregate.Version,
            events,
            cancellationToken: cancellationToken
        );

        return result.NextExpectedStreamRevision.ToUInt64();
    }

    public Task<ulong> DeleteAsync<T>(Guid id, T aggregate, ulong? version = null,
        CancellationToken cancellationToken = default) where T : class, IAggregate
    {
        return UpdateAsync(id, aggregate, version, cancellationToken);
    }
}