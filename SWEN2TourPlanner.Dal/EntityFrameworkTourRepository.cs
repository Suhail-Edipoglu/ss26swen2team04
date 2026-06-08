using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkTourRepository : ITourRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    
    public EntityFrameworkTourRepository(SWEN2TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<IEnumerable<Tour>> GetAllToursAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<Tour?> GetTourByIdAsync(string username, int tourId)
    {
        throw new NotImplementedException();
    }

    public Task InsertTourAsync(string username, Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task UpdateTourAsync(string username, Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTourAsync(string username, int tourId)
    {
        throw new NotImplementedException();
    }
}