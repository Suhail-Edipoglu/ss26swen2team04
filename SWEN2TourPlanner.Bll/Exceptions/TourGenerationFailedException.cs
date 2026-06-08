namespace SWEN2TourPlanner.Bll.Exceptions;

[Serializable]
public class TourGenerationFailedException : Exception
{
    public TourGenerationFailedException()
    {
    }

    public TourGenerationFailedException(string? message) : base(message)
    {
    }

    public TourGenerationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}