using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface IApiService {
        // TOURS
        Task<List<Tour>> GetToursAsync();
        Task<List<Tour>> SearchToursAsync(string searchTerm);
        Task<Tour> GetTourByIdAsync(int tourId);
        Task<int> CreateTourAsync(Tour tourData); // returns id
        Task<bool> UpdateTourAsync(Tour tourData);
        Task<bool> DeleteTourAsync(int tourId);
        // TOUR LOGS
        Task<List<TourLog>> GetTourLogsAsync(int tourId);
        Task<List<TourLog>> SearchTourLogsAsync(int tourId, string searchTerm);
        Task<TourLog> GetTourLogByIdAsync(int tourLogId);
        Task<int> CreateTourLogAsync(TourLog tourLogData); // returns id
        Task<bool> UpdateTourLogAsync(TourLog tourLogData);
        Task<bool> DeleteTourLogAsync(int tourLogId);
}
