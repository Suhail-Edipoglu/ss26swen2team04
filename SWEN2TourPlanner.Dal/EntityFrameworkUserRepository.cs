using Microsoft.EntityFrameworkCore;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkUserRepository : IUserRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    
    public EntityFrameworkUserRepository(SWEN2TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task InsertUserAsync(User user)
    {
        try
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            throw new DuplicateKeyException($"User with username '{user.Username}' already exists.", e);
        }
    }
}