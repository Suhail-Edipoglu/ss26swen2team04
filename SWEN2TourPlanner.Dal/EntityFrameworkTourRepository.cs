using Microsoft.EntityFrameworkCore;
using SWEN2TourPlanner.Models;
using Microsoft.Extensions.Logging;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkTourRepository : ITourRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    private readonly ILogger<EntityFrameworkTourRepository> _logger;
    
    public EntityFrameworkTourRepository(SWEN2TourPlannerDbContext dbContext, ILogger<EntityFrameworkTourRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<List<Tour>> GetAllToursAsync(string username)
    {
        try
        {
            var tours = await _dbContext.Tours
                .Include(t => t.User)
                .AsNoTracking()
                .Where(t => t.User.Username == username)
                .ToListAsync();
            return tours;
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            return null;
        }
    }

    public async Task<Tour?> GetTourByIdAsync(string username, int tourId)
    {
        var tour = await _dbContext.Tours
            .Include(t => t.User)
            .SingleOrDefaultAsync(t => t.Id == tourId && t.User.Username == username);
        
        return tour;
    }

    public async Task InsertTourAsync(string username, Tour tour)
    {
        try
        {
            var user = await _dbContext.Users.SingleAsync(u => u.Username == username);
            tour.User = user;
            await _dbContext.Tours.AddAsync(tour);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            _logger.LogWarning(e, "Failed to insert tour with name '{tour.Name}' for user '{username}'.",  tour.Name, username);
            
            throw new DuplicateKeyException($"Tour with name '{tour.Name}' already exists for user '{username}'.", e);
        }
    }

    public async Task UpdateTourAsync(string username, Tour tour)
    {
        var user = await _dbContext.Users.SingleAsync(u => u.Username == username);
        tour.User = user;
        var existingTour = await _dbContext.Tours.SingleOrDefaultAsync(t => t.Id == tour.Id && t.User.Username == username);
        if (existingTour == null)
        {
            throw new KeyNotFoundException($"Tour with id '{tour.Id}' not found for user '{username}'.");
        }
        
        existingTour.Name = tour.Name;
        existingTour.Description = tour.Description;
        existingTour.From = tour.From;
        existingTour.To = tour.To;
        existingTour.TransportType = tour.TransportType;
        existingTour.Distance = tour.Distance;
        existingTour.EstimatedTime = tour.EstimatedTime;
        existingTour.RouteInformation = tour.RouteInformation;
        
        try
        {
            _dbContext.Tours.Update(existingTour);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            throw new DuplicateKeyException($"Tour with name '{tour.Name}' already exists for user '{username}'.", e);
        }
    }

    public async Task<bool> DeleteTourAsync(string username, int tourId)
    {
        var tour = await _dbContext.Tours.SingleOrDefaultAsync(t => t.Id == tourId && t.User.Username == username);
        if (tour == null)
        {
            _logger.LogWarning("Delete Tour failed: Tour with id '{tourId}' not found for user '{username}'.", tourId, username);
            return false;
        }
        
        _dbContext.Tours.Remove(tour);
        await _dbContext.SaveChangesAsync();
        
        _logger.LogInformation("Tour with id '{tourId}' deleted for user '{username}'.", tourId, username);
        
        return true;
    }
    
    public async Task<List<Tour>> ExportToursAsync(string username)
    {
        var tours = await _dbContext.Tours
            .Include(t => t.User)
            .Include(t => t.Logs)
            .AsNoTracking()
            .Where(t => t.User.Username == username)
            .ToListAsync();
        return tours;
    }

    public async Task<bool> ImportToursAsync(string username, List<Tour> tours)
    {
        var user = await _dbContext.Users.SingleAsync(u => u.Username == username);
        foreach (var tour in tours)
        {
            tour.User = user;
            tour.UserId = user.Id;
            tour.Id = 0;

            if (tour.Logs != null && tour.Logs.Count > 0)
            {
                foreach (var log in tour.Logs)
                {
                    log.TourId = 0;
                    log.Id = 0;
                }
            }
        }

        try
        {
            _dbContext.Tours.AddRangeAsync(tours);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e) when (e is DbUpdateException || e is ArgumentException)
        {
            _logger.LogWarning(e, "Failed to import tours for user '{username}'.", username);
            return false;
        }
    }
}