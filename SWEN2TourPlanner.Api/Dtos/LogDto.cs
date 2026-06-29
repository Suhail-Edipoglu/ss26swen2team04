using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Api.Dtos;
// todo dtos
public class LogDto
{
    public int Id { get; set; }
    public required int TourId { get; set; }
    public required DateTime Time { get; set; }
    public required string Comment { get; set; }
    public required float Difficulty { get; set; }
    public required int TotalDistance { get; set; }
    public required TimeOnly TotalTime { get; set; }
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