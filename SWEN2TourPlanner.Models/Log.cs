namespace SWEN2TourPlanner.Models;

public class Log
{
    public Log()
    {
    }

    public Log(int id, DateTime time, string comment, float difficulty, int totalDistance, TimeOnly totalTime,
        float rating, int tourId, Tour? tour) : this()
    {
        Id = id;
        Time = time;
        Comment = comment;
        Difficulty = difficulty;
        TotalDistance = totalDistance;
        TotalTime = totalTime;
        Rating = rating;
        TourId = tourId;
        Tour = tour;
    }

    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string Comment { get; set; }
    public float Difficulty { get; set; }
    public int TotalDistance { get; set; }
    public TimeOnly TotalTime { get; set; }
    public float Rating { get; set; }
    public int TourId { get; set; }
    public Tour? Tour { get; set; }
}