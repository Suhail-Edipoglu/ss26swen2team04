using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public interface IUserRepository
{
    Task<User?> GetUserByUsernameAsync(string username);
    Task InsertUserAsync(User user);
}