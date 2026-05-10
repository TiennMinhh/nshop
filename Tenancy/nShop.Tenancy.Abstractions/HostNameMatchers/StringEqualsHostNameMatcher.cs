namespace nShop.Tenancy.Abstractions.HostNameMatchers;

public class StringEqualsHostNameMatcher : IHostNameMatcher
{
    public bool IsMatch(string hostName, string pattern)
    {
        return string.Equals(hostName, pattern, StringComparison.OrdinalIgnoreCase);
    }

    public static StringEqualsHostNameMatcher Instance { get; } = new();
}