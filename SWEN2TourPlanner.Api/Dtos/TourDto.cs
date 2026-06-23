using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Api.Dtos;
// todo split into diff dtos for create, response etc
public class TourDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string From { get; set; }
    public required string To { get; set; }
    public required TransportType TransportType { get; set; }
    public required int Distance { get; set; }
    public required TimeOnly EstimatedTime { get; set; }
    public required string RouteInformation { get; set; }
    public int UserId { get; set; }
    public List<LogDto>? Logs { get; set; }
}

public static class TourDtoExtensions
{
    public static TourDto ToDto(this Tour tour)
    {
        return new TourDto
        {
            Id = tour.Id,
            Name = tour.Name,
            Description = tour.Description,
            From = tour.From,
            To = tour.To,
            TransportType = tour.TransportType,
            Distance = tour.Distance,
            EstimatedTime = tour.EstimatedTime,
            RouteInformation = tour.RouteInformation,
            UserId = tour.UserId,
            Logs = tour.Logs?.Select(l => l.ToDto()).ToList()
        };
    }

    public static Tour ToTour(this TourDto tourDto)
    {
        return new Tour
        {
            Id = tourDto.Id,
            Name = tourDto.Name,
            Description = tourDto.Description,
            From = tourDto.From,
            To = tourDto.To,
            TransportType = tourDto.TransportType,
            Distance = tourDto.Distance,
            EstimatedTime = tourDto.EstimatedTime,
            RouteInformation = tourDto.RouteInformation,
            UserId = tourDto.UserId,
            Logs = tourDto.Logs?.Select(l => l.ToLog()).ToList()
        };
    }
}