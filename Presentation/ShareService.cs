using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Presentation;
public class ShareService(NavigationManager navigation, ClipboardService clipboardService)
{
    public async Task<Result> CopyUrlToClipboardAsync(Dictionary<string, string?> parameters)
    {
        try
        {
            // Get current URL components
            var uri = new Uri(navigation.Uri);
            var basePath = uri.GetLeftPart(UriPartial.Path);
            var existingQuery = QueryHelpers.ParseQuery(uri.Query);

            // Merge parameters (new values override existing ones)
            var mergedParams = existingQuery
                .ToDictionary(k => k.Key, v => v.Value.ToString())
                .Union(parameters.Where(x => !string.IsNullOrEmpty(x.Value)))
                .ToDictionary(k => k.Key, v => v.Value);

            // Build new URL
            var newUrl = QueryHelpers.AddQueryString(basePath, mergedParams);
            var result = await clipboardService.CopyAsync(newUrl);
            return result;
        }
        catch (Exception ex)
        {
            return (Error)ex;
        }
    }
}