using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class LoginManager(IApiService api) : ILoginManager {
    private string? _token;
    private DateTime _tokenValidUntil = DateTime.MinValue;
    private UserData? _userData;
    private readonly IApiService _api = api;
    
    public async Task<bool> LoginAsync(UserData userData) {
        try {
            var token = await _api.LoginAsync(userData);
            _token = token;
            
            if (_token != null) {
                _tokenValidUntil = DateTime.Now.AddMinutes(50);
                _userData = userData;
                return true;
            }
            return false;
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Login failed: {ex.Message}");
            return false;
        }
    }
    
    public async Task<bool> RegisterAsync(UserData userData) {
        try {
            return await _api.RegisterAsync(userData);
        } catch (Exception ex) {
            System.Diagnostics.Debug.WriteLine($"Register failed: {ex.Message}");
            return false;
        }
    }

    public void Logout() {
        _token = null;
        _userData = null;
    }

    public bool IsLoggedIn() {
        return _token != null;
    }

    public async Task<string?> GetTokenAsync() {
        if (_token != null && _tokenValidUntil < DateTime.Now) {
            // updates token if user still logged in
            if (_userData != null) {
                await LoginAsync(_userData);
            }
        }
        return _token;
    }
}