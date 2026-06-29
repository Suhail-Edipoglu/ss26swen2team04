using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Models.Interfaces;

public interface ILoginManager {
    Task<bool> LoginAsync(UserData userData);
    Task<bool> RegisterAsync(UserData userData);
    void Logout();
    bool IsLoggedIn();
    Task<string?> GetTokenAsync();
}
