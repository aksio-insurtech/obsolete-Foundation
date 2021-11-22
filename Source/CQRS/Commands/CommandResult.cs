namespace Aksio.Commands
{
    /// <summary>
    /// Represents the result coming from executing a command.
    /// </summary>
    /// <param name="IsOk">Whether or not everything is Ok.</param>
    public record CommandResult(bool IsOk);
}
