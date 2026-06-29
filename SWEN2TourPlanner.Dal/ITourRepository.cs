using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public interface ITourRepository
{
    Task<List<Tour>> GetAllToursAsync(string username);
    Task<Tour?> GetTourByIdAsync(string username, int tourId);
    Task InsertTourAsync(string username, Tour tour);
    Task UpdateTourAsync(string username, Tour tour);
    Task<bool> DeleteTourAsync(string username, int tourId);
}