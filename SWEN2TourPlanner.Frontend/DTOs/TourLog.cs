namespace SWEN2TourPlanner.Frontend.DTOs;

public class TourLog {
    public int? Id { get; set; }
    public DateTime Time { get; set; }
    public required string Comment { get; set; }
    public float Difficulty { get; set; }
    public int TotalDistance { get; set; }
    public TimeSpan TotalTime { get; set; }
    public float Rating { get; set; }
    public int TourId { get; set; }
}