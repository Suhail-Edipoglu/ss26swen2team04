using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Models.Interfaces;

public interface ICache {
    public Tour CurrentTour { get; set; }
    public TourLog CurrentTourLog { get; set; }
}
