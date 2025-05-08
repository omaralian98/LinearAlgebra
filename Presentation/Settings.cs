namespace Presentation;

public static class Settings
{
    public static MathJaxSettings MathJaxSettings { get; set; } = new();
    public static int MaxRowsAllowed { get; set; } = 8;
    public static int MaxColumnsAllowed { get; set; } = 8;

    public static bool ShowErrorMessageWhenExceedingMaxRowsOrColumnsAllowed { get; set; } = true;

    public static int Upperbound { get; set; } = 10;
    public static int Lowerbound { get; set; } = -9;

    public static bool ShowSteps { get; set; } = true;

}


public class MathJaxSettings
{
    public bool ShowNegativeBesideTheFractionBar { get; set; } = true;
    public bool DiagonalFractions { get; set; } = false;
    public Display DefaultFractionConfiguration { get; set; } = Display.Inline;
    
    public bool UseInlineModeForMatrices { get; set; } = true;

    public bool ForceBreakModeForMatrices { get; set; } = false;
}