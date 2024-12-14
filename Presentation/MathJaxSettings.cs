namespace Presentation;

public static class MathJaxSettings
{
    public static bool ShowNegativeBesideTheFractionBar { get; set; } = true;
    public static bool DiagonalFractions { get; set; } = false;
    public static Display DefaultFractionConfiguration { get; set; } = Display.Inline;

    public static bool UseInlineModeForMatrices { get; set; } = true;
}

public enum Display
{
    None,
    Inline,
    Block
}