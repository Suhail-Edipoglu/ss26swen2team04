using SWEN2TourPlanner.Dal;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public Task<User> GetUserByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<User> RegisterUserAsync(string username, string hashedPassword)
    {
        throw new NotImplementedException();
    }
}