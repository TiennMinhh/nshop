namespace nShop.Core;

public interface IProjection
{
    void Project(object @event);
}

public interface IProjection<in TEvent> : IProjection where TEvent : class
{
    void Project(TEvent @event);

    void IProjection.Project(object @event)
    {
        if (@event is TEvent typedEvent)
            Project(typedEvent);
    }
}