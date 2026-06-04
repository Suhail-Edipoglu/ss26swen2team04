using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface IApiService {
        // AUTH
        Task<bool> RegisterAsync(LoginData loginData);
        Task<bool> LoginAsync(LoginData loginData);
        // TOURS
        Task<List<Tour>> GetToursAsync();
        Task<string> CreateTourAsync(string token, string tourData);
        // TOUR LOGS
        Task<string> GetTourLogsAsync(int tourId);
        Task<string> CreateTourLogAsync(string token, int tourId, string tourLogData);
        Task<string> UpdateTourAsync(string token, int tourId, string tourData);
        Task<string> UpdateTourLogAsync(string token, int tourLogId, string tourLogData);
        Task<string> DeleteTourAsync(string token, int tourId);
        Task<string> DeleteTourLogAsync(string token, int tourLogId);
}