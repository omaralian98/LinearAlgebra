using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Presentation;

public class MathJaxBaseComponent : ComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        await RenderMath();
    }

    protected async Task RenderMath()
    {
        await JSRuntime.InvokeVoidAsync("MathJax.typeset");
    }

    public async ValueTask DisposeAsync()
    {
        await JSRuntime.InvokeVoidAsync("MathJax.typesetClear");
    }
}