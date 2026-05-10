namespace nShop.Tenancy.Abstractions;

public interface IHostNameMatcher
{
    bool IsMatch(string hostName, string pattern);
}