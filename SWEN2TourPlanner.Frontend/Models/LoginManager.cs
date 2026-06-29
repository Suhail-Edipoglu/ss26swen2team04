using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class LoginManager(IApiService api) : ILoginManager {
    private string? _token;
    private DateTime _tokenValidUntil = DateTime.MinValue;
    private UserData? _userData;
    private readonly IApiService _api = api;
    
    public bool Login(UserData userData) {
        try {
            var task = _api.LoginAsync(userData);
            task.Wait();
            _token = task.Result;
            
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
    
    public bool Register(UserData userData) {
        try {
            var task = _api.RegisterAsync(userData);
            task.Wait();
            return task.Result;
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

    public string? GetToken() {
        if (_token != null && _tokenValidUntil < DateTime.Now) {
            // updates token if user still logged in
            if (_userData != null) {
                Login(_userData);
            }
        }
        return _token;
    }
}