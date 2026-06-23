namespace SWEN2TourPlanner.Frontend.DTOs;

public class Tour
{
    public int? Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required TransportType TransportType { get; set; }
    public required int Distance { get; set; }
    public required TimeOnly EstimatedTime  { get; set; }
    public required string RouteInformation { get; set; }
    public required int UserId { get; set; }
}