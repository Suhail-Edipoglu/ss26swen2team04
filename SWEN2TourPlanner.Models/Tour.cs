namespace SWEN2TourPlanner.Models;

public class Tour
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string From { get; set; }
    public string To { get; set; }
    public TransportType TransportType { get; set; }
    public int Distance { get; set; }
    public TimeOnly EstimatedTime  { get; set; }
    public string RouteInformation { get; set; }
    public string ImgPath  { get; set; }
    public int UserId { get; set; }
    public List<Log>? Logs { get; set; }

    public Tour()
    {
    }

    public Tour(int id, string name, string description, string from, string to, TransportType transportType, int distance,
        TimeOnly estimatedTime, string routeInformation, string imgPath, int userId, List<Log>? logs) : this()
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
        ImgPath = imgPath;
        UserId = userId;
        Logs = logs;
    }
}