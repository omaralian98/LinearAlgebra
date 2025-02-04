﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Presentation;

public class MathJaxBaseComponent : ComponentBase, IAsyncDisposable
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public string TextToRender { get; set; } = string.Empty;

    protected ElementReference ElementReference { get; set; }

    private string RenderedText { get; set; } = string.Empty;


    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && TextToRender is not null)
        {
            await RenderMath(TextToRender);
            RenderedText = (string)TextToRender.Clone();
        }
        else if (TextToRender is not null && !string.IsNullOrWhiteSpace(RenderedText) && !TextToRender.Equals(RenderedText))
        {
            await RenderMath(TextToRender);
            RenderedText = (string)TextToRender.Clone();
        }
    }

    protected async override Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
    }

    protected async Task RenderMath(string text)
    {
        await JSRuntime.InvokeVoidAsync("ReplaceElement", ElementReference, text);
    }

    protected async Task Clear()
    {
        await JSRuntime.InvokeVoidAsync("MathJax.typesetClear", ElementReference);
    }

    public async ValueTask DisposeAsync()
    {
        await Clear();
    }
}