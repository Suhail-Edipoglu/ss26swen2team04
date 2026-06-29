using System.Text.Json;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Services;

public class ApiService(HttpClient httpClient) : IApiService {
    private static readonly JsonSerializerOptions JsonOptions = new() {
        PropertyNameCaseInsensitive = true
    };
    
    // AUTH
    public async Task<bool> RegisterAsync(UserData userData) {
        try {
            var response = await httpClient.PostAsJsonAsync("api/auth/register", userData);
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    public async Task<string?> LoginAsync(UserData userData) {
        try {
            var response = await httpClient.PostAsJsonAsync("api/auth/login", userData);
            if (response.IsSuccessStatusCode) {
                var content = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(content);
                
                // Try to get token property (case-insensitive)
                if (doc.RootElement.TryGetProperty("token", out var tokenElement) ||
                    doc.RootElement.TryGetProperty("Token", out tokenElement)) {
                    return tokenElement.GetString();
                }
            }
            return null;
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Login error: {ex.Message}");
            return null;
        }
    }

    // TOURS
    public async Task<List<Tour>> GetToursAsync() {
        try {
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
            var response = await httpClient.PutAsJsonAsync($"api/tours/{tourData.Id}", tourData);
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    public async Task<bool> DeleteTourAsync(int tourId) {
        try {
            var response = await httpClient.DeleteAsync($"api/tours/{tourId}");
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    // TOUR LOGS
    public async Task<List<TourLog>> GetTourLogsAsync(int tourId) {
        try {
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
            var response = await httpClient.GetAsync($"api/logs/{tourLogId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TourLog>(content, JsonOptions)!;
        } catch {
            throw new HttpRequestException($"Failed to get tour log with id {tourLogId}");
        }
    }

    public async Task<int> CreateTourLogAsync(TourLog tourLogData) {
        try {
            var response = await httpClient.PostAsJsonAsync("api/logs", tourLogData);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<int>(content, JsonOptions);
        } catch {
            throw new HttpRequestException("Failed to create tour log");
        }
    }

    public async Task<bool> UpdateTourLogAsync(TourLog tourLogData) {
        try {
            var response = await httpClient.PutAsJsonAsync($"api/logs/{tourLogData.Id}", tourLogData);
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

    public async Task<bool> DeleteTourLogAsync(int tourLogId) {
        try {
            var response = await httpClient.DeleteAsync($"api/logs/{tourLogId}");
            return response.IsSuccessStatusCode;
        } catch {
            return false;
        }
    }

}