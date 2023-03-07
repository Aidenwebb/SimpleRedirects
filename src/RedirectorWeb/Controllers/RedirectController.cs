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
        var http_scheme = Request.IsHttps ? "https://" : "http://";
        var fqdn = Request.Host.Value;
        var path = Request.Path.Value;
        var queryString = Request.QueryString.Value;

        var requestUri = http_scheme + fqdn + path + queryString;

        var uri = new Uri(requestUri);
        var redirectionResult = RedirectHandler.RedirectToApexWww(uri);

        return RedirectPermanent(redirectionResult.ToString());
    }
}