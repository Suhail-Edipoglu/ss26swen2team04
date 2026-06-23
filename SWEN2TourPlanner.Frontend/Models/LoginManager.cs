using SWEN2TourPlanner.Frontend.DTOs;
using SWEN2TourPlanner.Frontend.Models.Interfaces;
using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Models;

public class LoginManager(IApiService api) : ILoginManager {
    bool ILoginManager.IsLoggedIn() {
        throw new NotImplementedException();
    }
    public string? Token { get; private set; } = null;
    private IApiService _api = api; 
    
    public bool Login(UserData userData) {
        var success = _api.LoginAsync(userData);
        Token = success.Result;
        return Token != null;
    }
    
    public bool Register(UserData userData) {
        var success = _api.RegisterAsync(userData);
        return success.Result;
    }

    public void Logout() {
        Token = null;
    }

    public bool IsLoggedIn() {
        return Token != null;
    }
    
}