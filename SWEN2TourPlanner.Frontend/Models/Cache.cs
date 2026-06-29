using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class Cache : ICache {
    public Tour? CurrentTour { get; set; }
    
    public TourLog? CurrentTourLog { get; set; }
}