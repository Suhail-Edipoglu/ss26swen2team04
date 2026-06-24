using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Models.Interfaces;

public interface ILoginManager {
    public bool Login(UserData userData);
    public bool Register(UserData userData);
    public void Logout();
    public bool IsLoggedIn();
    public string? GetToken();
}