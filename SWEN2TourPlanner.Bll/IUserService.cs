using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public interface IUserService
{
    Task<User> GetUserByUsernameAsync(string username);
    Task<User> RegisterUserAsync(string username, string hashedPassword);
}