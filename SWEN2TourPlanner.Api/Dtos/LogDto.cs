using System.ComponentModel.DataAnnotations;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Api.Dtos;
// todo dtos
public class LogDto
{
    public int Id { get; set; }
    public required int TourId { get; set; }
    public required DateTimeOffset Time { get; set; }
    [Required]
    [StringLength(500)]
    public required string Comment { get; set; }
    [Required]
    [Range(1, 10)]
    public required float Difficulty { get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public required int TotalDistance { get; set; }
    public required TimeSpan TotalTime { get; set; }
    [Required]
    [Range(1, 5)]
    public required float Rating { get; set; }
}

public static class LogDtoExtensions
{
    public static LogDto ToDto(this Log log)
    {
        return new LogDto
        {
            Id = log.Id,
            TourId = log.TourId,
            Time = log.Time,
            Comment = log.Comment,
            Difficulty = log.Difficulty,
            TotalDistance = log.TotalDistance,
            TotalTime = log.TotalTime,
            Rating = log.Rating
        };
    }

    public static Log ToLog(this LogDto logDto)
    {
        return new Log
        {
            Id = logDto.Id,
            TourId = logDto.TourId,
            Time = logDto.Time,
            Comment = logDto.Comment,
            Difficulty = logDto.Difficulty,
            TotalDistance = logDto.TotalDistance,
            TotalTime = logDto.TotalTime,
            Rating = logDto.Rating
        };
    }
}