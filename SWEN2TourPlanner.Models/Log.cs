namespace SWEN2TourPlanner.Models;

public class Log
{
    public int Id { get; set; }
    public DateTime Time { get; set; }
    public string Comment { get; set; }
    public float Difficulty { get; set; }
    public int TotalDistance { get; set; }
    public int TotalTime { get; set; }
    public int Rating { get; set; }
}