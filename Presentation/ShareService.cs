using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Presentation;
public class ShareService(NavigationManager navigation)
{
    public Result<string> GetUrl(Dictionary<string, string?> parameters)
    {
        try
        {
            var uri = new Uri(navigation.Uri);
            var basePath = uri.GetLeftPart(UriPartial.Path);
            var existingQuery = QueryHelpers.ParseQuery(uri.Query);

            var newUrl = QueryHelpers.AddQueryString(basePath, parameters);
            return newUrl;
        }
        catch (Exception ex)
        {
            return (Error)ex;
        }
    }
}