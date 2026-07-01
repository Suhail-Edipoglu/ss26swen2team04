using System.ComponentModel.DataAnnotations;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Api.Dtos;
// todo split into diff dtos for create, response etc
public class TourDto
{
    public int Id { get; set; }
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
    public required int? Distance { get; set; }
    public required TimeSpan? EstimatedTime { get; set; }
    public required string RouteInformation { get; set; }
    [Range(0, 100)]
    public float Popularity { get; set; } = 0;
    [Range(0, 100)]
    public float ChildFriendliness { get; set; } = 0;
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
            Popularity = tour.Popularity,
            ChildFriendliness = tour.ChildFriendliness,
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
            Popularity = tourDto.Popularity,
            ChildFriendliness = tourDto.ChildFriendliness,
            UserId = tourDto.UserId,
            Logs = tourDto.Logs?.Select(l => l.ToLog()).ToList()
        };
    }
}