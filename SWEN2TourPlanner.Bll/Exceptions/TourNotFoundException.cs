namespace SWEN2TourPlanner.Bll.Exceptions;

[Serializable]
public class TourNotFoundException : Exception
{
    public TourNotFoundException()
    {
    }

    public TourNotFoundException(string? message) : base(message)
    {
    }

    public TourNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}