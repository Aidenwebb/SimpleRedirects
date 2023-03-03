using SimpleRedirects.Core;
namespace Core.Tests;

public class RedirectHandlerTests
{
    [Fact]
    public void RedirectToWww_Success()
    {
        Uri input = new Uri("https://example.com");
        Uri expected = new Uri("https://www.example.com");
        
        Uri actual = RedirectHandler.RedirectToApexWww(input);
        
        Assert.Equal(expected, actual);
   
    }

    public static IEnumerable<object[]> GenerateRedirectApexToWwwCases = new[]
    {
        new object[]
        {
            new Uri("https://example.com"), // Input Uri
            new Uri("https://www.example.com") // Expected Uri
        },
        new object[]
        {
            new Uri("https://example.co.uk"), // Input Uri
            new Uri("https://www.example.co.uk") // Expected Uri
        },
        new object[]
        {
            new Uri("https://www.example.com"), // Input Uri
            new Uri("https://www.example.com") // Expected Uri
        },
        new object[]
        {
            new Uri("https://www.example.co.uk"), // Input Uri
            new Uri("https://www.example.co.uk") // Expected Uri
        },
        new object[]
        {
            new Uri("https://example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
        },
        new object[]
        {
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
        },
        new object[]
        {
            new Uri("https://othersubdomain.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
        }
        
    };
    
    [Theory]
    [MemberData(nameof(GenerateRedirectApexToWwwCases))]
    public void RedirectToWww_WithInputs_Success(Uri input, Uri expected)
    {
        Uri actual = RedirectHandler.RedirectToApexWww(input);
        
        Assert.Equal(expected, actual);
    }
}