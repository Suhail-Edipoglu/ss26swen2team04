namespace SWEN2TourPlanner.Models;

public class Tour
{
    public Tour()
    {
    }

    public Tour(int id, string name, string description, string from, string to, TransportType transportType,
        int distance,
        TimeSpan estimatedTime, string routeInformation, int userId, List<Log>? logs) : this()
    {
        Id = id;
        Name = name;
        Description = description;
        From = from;
        To = to;
        TransportType = transportType;
        Distance = distance;
        EstimatedTime = estimatedTime;
        RouteInformation = routeInformation;
        UserId = userId;
        Logs = logs;
    }

    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required TransportType TransportType { get; set; }
    public required int Distance { get; set; }
    public required TimeSpan EstimatedTime { get; set; }
    public required string RouteInformation { get; set; }
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<Log>? Logs { get; set; }
}