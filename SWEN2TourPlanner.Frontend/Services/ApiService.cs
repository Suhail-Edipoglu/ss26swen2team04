using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Services;

public class ApiService(HttpClient httpClient) : IApiService {
    private readonly HttpClient _httpClient = httpClient;
    
    public async Task<bool> RegisterAsync(UserData userData) {
        throw new NotImplementedException();
    }
    public async Task<string?> LoginAsync(UserData userData) {
        throw new NotImplementedException();
    }
    public async Task<List<Tour>> GetToursAsync() {
        throw new NotImplementedException();
    }
    public async Task<Tour> GetTourByIdAsync(int tourId) {
        throw new NotImplementedException();
    }
    public async Task<int> CreateTourAsync(Tour tourData) {
        throw new NotImplementedException();
    }
    public async Task<bool> UpdateTourAsync(Tour tourData) {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteTourAsync(int tourId) {
        throw new NotImplementedException();
    }
    public async Task<List<TourLog>> GetTourLogsAsync(int tourId) {
        throw new NotImplementedException();
    }
    public async Task<TourLog> GetTourLogByIdAsync(int tourLogId) {
        throw new NotImplementedException();
    }
    public async Task<int> CreateTourLogAsync(TourLog tourLogData) {
        throw new NotImplementedException();
    }
    public async Task<bool> UpdateTourLogAsync(TourLog tourLogData) {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteTourLogAsync(int tourLogId) {
        throw new NotImplementedException();
    }

}