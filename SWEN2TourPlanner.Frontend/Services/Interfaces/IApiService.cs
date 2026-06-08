using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface IApiService {
        // AUTH
        Task<bool> RegisterAsync(LoginData loginData); // returns success to use for redirect
        Task<bool> LoginAsync(LoginData loginData);
        // TOURS
        Task<List<Tour>> GetToursAsync();
        Task<Tour> GetTourByIdAsync(int tourId);
        Task<int> CreateTourAsync(Tour tourData); // returns id
        Task<bool> UpdateTourAsync(Tour tourData);
        Task<bool> DeleteTourAsync(int tourId);
        // TOUR LOGS
        Task<List<TourLog>> GetTourLogsAsync(int tourId);
        Task<TourLog> GetTourLogByIdAsync(int tourLogId);
        Task<int> CreateTourLogAsync(TourLog tourLogData); // returns id
        Task<bool> UpdateTourLogAsync(TourLog tourLogData);
        Task<bool> DeleteTourLogAsync(int tourLogId);
}
