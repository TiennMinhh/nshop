using System.Text.RegularExpressions;

namespace nShop.Tenancy.Abstractions.HostNameMatchers;

public class RegExHostNameMatcher : IHostNameMatcher
{
    public bool IsMatch(string hostName, string pattern)
    {
        return Regex.IsMatch(hostName, pattern);
    }

    public static RegExHostNameMatcher Instance { get; } = new();
}