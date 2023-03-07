using Nager.PublicSuffix;

namespace SimpleRedirects.Core;

public static class RedirectHandler
{
    /// <summary>
    ///     Takes a URI and replaces the Host portion with the www subdomain of the apex/registerable domain.
    /// </summary>
    /// <param name="uri"></param>
    /// <returns></returns>
    public static Uri RedirectToApexWww(Uri uri)
    {
        var parser = new DomainParser(new WebTldRuleProvider());
        var domainInfo = parser.Parse(uri.Host);

        var builder = new UriBuilder(uri)
        {
            Host = "www." + domainInfo.RegistrableDomain
        };
        return builder.Uri;
    }
}