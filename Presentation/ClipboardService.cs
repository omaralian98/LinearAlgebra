using Microsoft.JSInterop;

namespace Presentation;

public sealed class ClipboardService(IJSRuntime JS)
{
    public async Task<Result> CopyAsync(string text)
    {
        try
        {
            await JS.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (Exception ex)
        {
            return new Error($"{nameof(ClipboardService)}.{nameof(Copy)}", ex.Message);
        }
        return Result.Success();
    }

    public async Task<Result<string>> PasteAsync()
    {
        try
        {
            var result = await JS.InvokeAsync<string>("navigator.clipboard.readText");
            return result;
        }
        catch (Exception ex)
        {
            return new Error($"{nameof(ClipboardService)}.{nameof(Copy)}", ex.Message);
        }
    }

    public Result Copy(string text)
    {
        try
        {
            _ = JS.InvokeVoidAsync("navigator.clipboard.writeText", text).AsTask();
        }
        catch (Exception ex)
        {
            return new Error($"{nameof(ClipboardService)}.{nameof(Copy)}", ex.Message);
        }
        return Result.Success();
    }

    public Result<string> Paste()
    {
        try
        {
            var result = JS.InvokeAsync<string>("navigator.clipboard.readText").AsTask();
            return result.Result;
        }
        catch (Exception ex)
        {
            return new Error($"{nameof(ClipboardService)}.{nameof(Copy)}", ex.Message);
        }
    }
}
