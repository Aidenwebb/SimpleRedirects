# Simple Redirects app

Initially created to work around a limitation of Azure Front Door, [where directing an Apex domain to Azure Front Door requires periodic rotating of _dnsauth TXT records][afd-cert-docs], which is not ideal when you may not have access to modify the DNS records regularly.

Easier to just redirect the apex domain to the www.subdomain, and then use Azure Front Door to handle that subdomain.

This is a simple app that accepts HTTP/HTTPS requests and issues a 301 redirect, replacing the domain/host portion of the request with the www.subdomain of the apex domain.

## Example

| Request                                                | Redirect                                                   |
| ------------------------------------------------------ | ---------------------------------------------------------- |
| `https://example.com`                                  | `https://www.example.com`                                  |
| `https://example.co.uk/`                               | `https://www.example.co.uk/`                               |
| `https://othersubdomain.example.com/`                  | `https://www.example.com/`                                 |
| `https://example.com:8080`                             | `https://www.example.com:8080`                             |
| `https://example.com:8080/path/?name=cat&food=carrots` | `https://www.example.com:8080/path/?name=cat&food=carrots` |
|                                                        |



<!-- Links:  -->

[afd-cert-docs]: https://learn.microsoft.com/en-us/azure/frontdoor/apex-domain#azure-front-door-managed-tls-certificate-rotation