using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public interface ITourService
{
    Task<Tour?> GetTourAsync(int tourId);
    Task<Tour?> GetToursAsync(string username);
    Task<Tour> CreateTourAsync(Tour tour);
    Task<bool> UpdateTourAsync(int tourId, Tour tour);
    Task<bool> RemoveTourAsync(int tourId);
    Task<IEnumerable<Tour>> FindMatchingToursAsync(string username, string? searchText = null);
}