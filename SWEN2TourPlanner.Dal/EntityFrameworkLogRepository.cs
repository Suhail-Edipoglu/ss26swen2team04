using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkLogRepository : ILogRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    
    public EntityFrameworkLogRepository(SWEN2TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<IEnumerable<Log>> GetAllLogsForTourAsync(string username, int tourId)
    {
        throw new NotImplementedException();
    }

    public Task<Log?> GetLogByIdAsync(string username, int logId)
    {
        throw new NotImplementedException();
    }

    public Task InsertLogAsync(string username, int tourId, Log log)
    {
        throw new NotImplementedException();
    }

    public Task UpdateLogAsync(string username, Log log)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteLogAsync(string username, int logId)
    {
        throw new NotImplementedException();
    }
}