namespace SWEN2TourPlanner.Bll;

[Serializable]
public class LogAlreadyExistsException : Exception
{
    public LogAlreadyExistsException()
    {
    }

    public LogAlreadyExistsException(string? message) : base(message)
    {
    }

    public LogAlreadyExistsException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}