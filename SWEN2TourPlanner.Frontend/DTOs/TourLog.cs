using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SWEN2TourPlanner.Frontend.DTOs;

public class TourLog {
    [JsonIgnore(Condition=JsonIgnoreCondition.WhenWritingNull)]
    public int? Id { get; set; }
    public DateTimeOffset Time { get; set; }
    [Required]
    [StringLength(500)]
    public required string Comment { get; set; }
    [Required]
    [Range(1, 10)]
    public float Difficulty { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public int TotalDistance { get; set; }
    public TimeSpan TotalTime { get; set; }
    [Required]
    [Range(1, 5)]
    public float Rating { get; set; }
    public int TourId { get; set; }
}