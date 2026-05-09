namespace nShop.Core;

public class DefaultStreamNameMapper : IStreamNameMapper
{
    public string Map(Guid id)
    {
        return id.ToString();
    }
    
    public static IStreamNameMapper Instance { get; } = new DefaultStreamNameMapper();
}