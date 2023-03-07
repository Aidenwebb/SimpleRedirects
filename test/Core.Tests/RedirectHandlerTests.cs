using SimpleRedirects.Core;

namespace Core.Tests;

public class RedirectHandlerTests
{
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
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple") // Input Uri
        },
        new object[]
        {
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple") // Input Uri
        },
        new object[]
        {
            new Uri("https://othersubdomain.example.co.uk/test/123/page?name=ferret&colour=purple"), // Input Uri
            new Uri("https://www.example.co.uk/test/123/page?name=ferret&colour=purple") // Input Uri
        }
    };

    [Fact]
    public void RedirectToWww_Success()
    {
        var input = new Uri("https://example.com");
        var expected = new Uri("https://www.example.com");

        var actual = RedirectHandler.RedirectToApexWww(input);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(GenerateRedirectApexToWwwCases))]
    public void RedirectToWww_WithInputs_Success(Uri input, Uri expected)
    {
        var actual = RedirectHandler.RedirectToApexWww(input);

        Assert.Equal(expected, actual);
    }
}