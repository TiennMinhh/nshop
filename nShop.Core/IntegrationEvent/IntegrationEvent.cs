using nShop.Shared;

namespace nShop.Core.IntegrationEvent;

public abstract class IntegrationEvent : IIntegrationEvent
{
    public Guid EventId { get;}

    public IntegrationEvent()
    {
        EventId = GuidHelpers.NewGuidVersion7();
    }

    public IntegrationEvent(Guid eventId)
    {
        EventId = eventId;
    }
}