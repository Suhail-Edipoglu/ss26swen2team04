using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface ITourService
{
    public List<Tour>? GetTours(int userId);
    public Tour? GetTourById(int tourId);
    public int? CreateTour(Tour tour);
    public void UpdateTour(Tour tour);
    public void DeleteTour(int tourId);
}