using System.Net.Http.Headers;
using System.Text.Json;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Services;

public class ApiService(HttpClient httpClient, ILoginManager loginManager) : IApiService {
    readonly ILoginManager _loginManager = loginManager;
    private static readonly JsonSerializerOptions JsonOptions = new() {
        PropertyNameCaseInsensitive = true
    };
    
    // TOURS
    public async Task<List<Tour>> GetToursAsync() {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync("api/tours");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Tour>>(content, JsonOptions) ?? new List<Tour>();
        } catch {
            return new List<Tour>();
        }
    }

    public async Task<List<Tour>> SearchToursAsync(string searchTerm) {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync($"api/tours?search={Uri.EscapeDataString(searchTerm)}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Tour>>(content, JsonOptions) ?? new List<Tour>();
        } catch {
            return new List<Tour>();
        }
    }

    public async Task<Tour> GetTourByIdAsync(int tourId) {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync($"api/tours/{tourId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Tour>(content, JsonOptions)!;
        } catch {
            throw new HttpRequestException($"Failed to get tour with id {tourId}");
        }
    }

    public async Task<int> CreateTourAsync(Tour tourData) {
        try {
            await SetAuthAsync();
            var response = await httpClient.PostAsJsonAsync("api/tours", tourData);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(content, JsonOptions);
        } catch {
            throw new HttpRequestException("Failed to create tour");
        }
    }

    public async Task<bool> UpdateTourAsync(Tour tourData) {
        try {
            await SetAuthAsync();
            var response = await httpClient.PutAsJsonAsync($"api/tours/{tourData.Id}", tourData);
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    public async Task<bool> DeleteTourAsync(int tourId) {
        try {
            await SetAuthAsync();
            var response = await httpClient.DeleteAsync($"api/tours/{tourId}");
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    // TOUR LOGS
    public async Task<List<TourLog>> GetTourLogsAsync(int tourId) {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync($"api/tours/{tourId}/logs");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TourLog>>(content, JsonOptions) ?? new List<TourLog>();
        } catch {
            return new List<TourLog>();
        }
    }

    public async Task<List<TourLog>> SearchTourLogsAsync(int tourId, string searchTerm) {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync($"api/tours/{tourId}/logs?search={Uri.EscapeDataString(searchTerm)}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<TourLog>>(content, JsonOptions) ?? new List<TourLog>();
        } catch {
            return new List<TourLog>();
        }
    }

    public async Task<TourLog> GetTourLogByIdAsync(int tourLogId) {
        try {
            await SetAuthAsync();
            var response = await httpClient.GetAsync($"api/logs/{tourLogId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TourLog>(content, JsonOptions)!;
        } catch {
            throw new HttpRequestException($"Failed to get tour log with id {tourLogId}");
        }
    }

    public async Task<int> CreateTourLogAsync(TourLog tourLogData) {
        tourLogData.Time = tourLogData.Time.ToUniversalTime();
        try {
            await SetAuthAsync();
            var response = await httpClient.PostAsJsonAsync("api/logs", tourLogData);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(content, JsonOptions);
        } catch {
            throw new HttpRequestException("Failed to create tour log");
        }
    }

    public async Task<bool> UpdateTourLogAsync(TourLog tourLogData) {
        tourLogData.Time = tourLogData.Time.ToUniversalTime();
        try {
            await SetAuthAsync();
            var response = await httpClient.PutAsJsonAsync($"api/logs/{tourLogData.Id}", tourLogData);
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    public async Task<bool> DeleteTourLogAsync(int tourLogId) {
        try {
            await SetAuthAsync();
            var response = await httpClient.DeleteAsync($"api/logs/{tourLogId}");
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }
    
    private async Task SetAuthAsync() {
        var token = await _loginManager.GetTokenAsync();
        if (!string.IsNullOrEmpty(token)) {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

}