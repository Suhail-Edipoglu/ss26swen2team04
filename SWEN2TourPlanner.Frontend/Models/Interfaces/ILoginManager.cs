using MudBlazor;
using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Models.Interfaces;

public interface ILoginManager {
    bool Login(UserData userData);
    bool Register(UserData userData);
    void Logout();
    bool IsLoggedIn();
    string? GetToken();
}
