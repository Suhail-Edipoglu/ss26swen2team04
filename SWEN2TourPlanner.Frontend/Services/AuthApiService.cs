using System.Text.Json;
using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Services;

public class AuthApiService(HttpClient httpClient) : IAuthApiService {
    
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

}