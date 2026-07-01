using Microsoft.EntityFrameworkCore;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkLogRepository : ILogRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    
    public EntityFrameworkLogRepository(SWEN2TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Log>> GetAllLogsForTourAsync(string username, int tourId)
    {
        try
        {
            var logs = await  _dbContext.Logs
                .Include(l => l.Tour)
                .ThenInclude(t => t.User)
                .Where(l => l.Tour.Id == tourId && l.Tour.User.Username == username)
                .ToListAsync();
            return logs;
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            return null;
        }
    }

    public async Task<Log?> GetLogByIdAsync(string username, int logId)
    {
        var log = await _dbContext.Logs
            .Include(l => l.Tour)
            .ThenInclude(t => t.User)
            .SingleOrDefaultAsync(l => l.Id == logId && l.Tour.User.Username == username);
        
        return log;
    }

    public async Task InsertLogAsync(string username, Log log)
    {
        try
        {
            var tour = await _dbContext.Tours
                .Include(t => t.User)
                .SingleAsync(t => t.Id == log.TourId && t.User.Username == username);
            log.Tour = tour;
            await _dbContext.Logs.AddAsync(log);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            throw new DuplicateKeyException($"Log with id '{log.Id}' already exists for user '{username}'.", e);
        }
    }

    public async Task UpdateLogAsync(string username, Log log)
    {
        var tour = await _dbContext.Tours
            .Include(t => t.User)
            .SingleAsync(t => t.Id == log.TourId && t.User.Username == username);
        log.Tour = tour;
        var existingLog = await _dbContext.Logs
            .SingleOrDefaultAsync(l => l.Id == log.Id && l.Tour.User.Username == username);
        if (existingLog == null)
        {
            throw new KeyNotFoundException($"Log with id '{log.Id}' not found for user '{username}'.");
        }
        _dbContext.Entry(existingLog).CurrentValues.SetValues(log);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteLogAsync(string username, int logId)
    {
        var log = await _dbContext.Logs
            .Include(l => l.Tour)
            .ThenInclude(t => t.User)
            .SingleOrDefaultAsync(l => l.Id == logId && l.Tour.User.Username == username);
        if (log == null)
        {
            return false;
        }
        _dbContext.Logs.Remove(log);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetLogsCountAsync(string username)
    {
        var allLogsCount = _dbContext.Logs
            .Include(l => l.Tour)
            .ThenInclude(t => t.User)
            .Where(l => l.Tour.User.Username == username)
            .Count();
        return allLogsCount > 0 ? allLogsCount : 0;
    }

    public async Task UpdateTourAttributesAsync(string username, int tourId, float popularity, float childFriendliness)
    {
        _dbContext.Tours.Single(t => t.Id == tourId && t.User.Username == username).Popularity = popularity;
        await _dbContext.SaveChangesAsync();
        
        _dbContext.Tours.Single(t => t.Id == tourId && t.User.Username == username).ChildFriendliness = childFriendliness;
        await _dbContext.SaveChangesAsync();
    }
}