namespace Presentation.Abstraction;

/// <summary>
/// Represents an error with a code and an optional description.
/// </summary>
public sealed record Error(string Code, string Description = "")
{
    /// <summary>
    /// Represents no error.
    /// </summary>
    public static readonly Error None = new(string.Empty, string.Empty);


    /// <summary>
    /// Converts an exception into an error
    /// </summary>
    public static explicit operator Error(Exception? exception) =>
        new("InternalError", exception?.Message);

}