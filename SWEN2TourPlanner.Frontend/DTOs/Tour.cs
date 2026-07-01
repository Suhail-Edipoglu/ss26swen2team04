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
    [JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]    
    public float? Popularity { get; set; }
    [JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
    public float? ChildFriendliness { get; set; }
}