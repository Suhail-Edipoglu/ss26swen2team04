using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public interface ITourService
{
    Task<Tour> GetTourAsync(string username, int tourId);
    Task<List<Tour>> GetToursAsync(string username);
    Task<Tour> CreateTourAsync(Tour tour,  string username);
    Task<bool> UpdateTourAsync(int tourId, Tour tour);
    Task<bool> RemoveTourAsync(string username, int tourId);
    Task<IEnumerable<Tour>> FindMatchingToursAsync(string username, string? searchText = null);
    Task<List<Tour>> ExportToursAsync(string username);
    Task<bool> ImportToursAsync(string username, List<Tour> tours);
}