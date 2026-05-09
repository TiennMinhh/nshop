using System.Collections.Concurrent;

namespace nShop.Core;

public class DefaultEventTypeNameMapper : IEventTypeNameMapper
{
    private readonly ConcurrentDictionary<string, Type> _typeCache = new();
    
    public Type? ToType(string type)
    {
        if (_typeCache.TryGetValue(type, out var t))
        {
            return t;
        }
        
        t = Type.GetType(type) ?? AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .FirstOrDefault(t => t.FullName == type);

        if (t != null)
            _typeCache.TryAdd(type, t);

        return t;
    }

    public string ToName(Type type)
    {
        return type.FullName ?? type.Name;
    }
    
    public static DefaultEventTypeNameMapper Instance { get; } = new();
}