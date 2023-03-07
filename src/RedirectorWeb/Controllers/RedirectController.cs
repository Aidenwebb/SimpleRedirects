using Microsoft.AspNetCore.Mvc;
using SimpleRedirects.Core;

namespace SimpleRedirects.RedirectorWeb.Controllers;

public class RedirectController : Controller
{
    [HttpGet]
    [Route("/{*segments}")]
    public IActionResult Index()
    {
        //Uri uri = new Uri();
        string http_scheme = Request.IsHttps ? "https://" : "http://";
        string fqdn = Request.Host.Value;
        string? path = Request.Path.Value;
        string? queryString = Request.QueryString.Value;

        string requestUri = http_scheme + fqdn + path + queryString;

        Uri uri = new Uri(requestUri);
        Uri redirectionResult = RedirectHandler.RedirectToApexWww(uri);

        return RedirectPermanent(redirectionResult.ToString());
    }
}