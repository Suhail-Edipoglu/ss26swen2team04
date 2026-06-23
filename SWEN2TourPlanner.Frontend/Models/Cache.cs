using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class Cache : ICache {
    public Tour CurrentTour { get; set; } = new Tour {Name = "ERROR", Description = "ERROR", Id = null, From = "ERROR", To = "ERROR", TransportType = TransportType.None, Distance = 0, EstimatedTime = new TimeOnly(0, 0), RouteInformation = "ERROR", UserId = 0};
    
    public TourLog CurrentTourLog { get; set; } = new TourLog {Id = null, Time = DateTime.UtcNow, Comment = "ERROR", Difficulty = 0, TotalDistance = 0, TotalTime = new TimeOnly(0, 0), Rating = 0, TourId = 0};
}