namespace SWEN2TourPlanner.Bll.Exceptions;

[Serializable]
public class LogGenerationFailedException : Exception
{
    public LogGenerationFailedException()
    {
    }

    public LogGenerationFailedException(string? message) : base(message)
    {
    }

    public LogGenerationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}