using SWEN2TourPlanner.Bll.Exceptions;
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
    
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        var user = await _userRepository.GetUserByUsernameAsync(username) ?? throw new UserNotFoundException($"User with username '{username}' not found.");

        return user;
    }

    public async Task<User> RegisterUserAsync(string username, string hashedPassword)
    {
        var user = new User
        {
            Username = username,
            HashedPassword = hashedPassword
        };
        
        try
        {
            await _userRepository.InsertUserAsync(user);
        }
        catch (DuplicateKeyException e)
        {
            throw new UserAlreadyExistsException($"User with username '{username}' already exists.", e);
        }
            
        return user;
    }
}