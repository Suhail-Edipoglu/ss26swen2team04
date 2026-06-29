using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class LoginManager(IApiService api) : ILoginManager {
    private string? _token = null;
    private DateTime _tokenValidUntil = DateTime.MinValue;
    private UserData? _userData = null;
    private readonly IApiService _api = api;
    
    public bool Login(UserData userData) {
        var success = _api.LoginAsync(userData);
        success.Wait();
        _token = success.Result;
        _tokenValidUntil = DateTime.Now.AddMinutes(50);
        _userData = userData;
        return _token != null;
    }
    
    public bool Register(UserData userData) {
        var success = _api.RegisterAsync(userData);
        success.Wait();
        return success.Result;
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
            Login(_userData!);
        }
        return _token;
    }
}