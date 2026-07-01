using System.Text.Json.Serialization;

namespace SWEN2TourPlanner.Frontend.DTOs;

public class Tour
{
    [JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required TransportType TransportType { get; set; }
    public int? Distance { get; set; }
    public TimeSpan? EstimatedTime  { get; set; }
    public required string RouteInformation { get; set; }
    public required int UserId { get; set; }
    public int? Popularity { get; set; }
    public int? ChildFriendliness { get; set; }
}