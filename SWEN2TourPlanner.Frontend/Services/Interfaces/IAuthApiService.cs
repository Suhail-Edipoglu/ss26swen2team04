using SWEN2TourPlanner.Frontend.DTOs;

namespace SWEN2TourPlanner.Frontend.Services.Interfaces;

public interface IAuthApiService {
    // AUTH
    Task<bool> RegisterAsync(UserData userData); // returns success to use for redirect
    Task<string?> LoginAsync(UserData userData); // returns token or null on fail

}