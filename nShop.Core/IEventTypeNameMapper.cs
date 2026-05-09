namespace nShop.Core;

public interface IEventTypeNameMapper
{
    Type? ToType(string type);
    string ToName(Type type);
}