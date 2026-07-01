using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWEN2TourPlanner.Frontend.DTOs;

public class Tour
{
    [JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }
    [Required]
    [StringLength(1000)]
    public required string Description { get; set; }
    [Required]
    [StringLength(100)]
    public required string From { get; set; }
    [Required]
    [StringLength(100)]
    public required string To { get; set; }
    [Required]
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