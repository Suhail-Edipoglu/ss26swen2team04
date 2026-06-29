namespace SWEN2TourPlanner.Bll.Exceptions;

[Serializable]
public class LogNotFoundException : Exception
{
    public LogNotFoundException()
    {
    }

    public LogNotFoundException(string? message) : base(message)
    {
    }

    public LogNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}