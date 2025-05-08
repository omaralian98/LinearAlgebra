using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Presentation;

public class EquationComponent : ComponentBase
{
    [Inject]
    private IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public string Equation { get; set; } = string.Empty;

    [Parameter]
    public Display Display { get; set; } = Display.Inline;

    public string Id { get; } = $"katex_{Guid.NewGuid():N}";


    protected async override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && string.IsNullOrWhiteSpace(Equation) == false)
        {
            await RenderMath(Equation);
        }
    }

    protected async Task RenderMath(string text)
    {
        try
        {
            bool isBlock = Display == Display.Block; 
            await JSRuntime.InvokeVoidAsync("Render", Id, text, isBlock);
        }
        catch { }
    }
}