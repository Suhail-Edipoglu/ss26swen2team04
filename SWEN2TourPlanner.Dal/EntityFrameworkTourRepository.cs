using Microsoft.EntityFrameworkCore;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Dal;

public class EntityFrameworkTourRepository : ITourRepository
{
    private readonly SWEN2TourPlannerDbContext _dbContext;
    
    public EntityFrameworkTourRepository(SWEN2TourPlannerDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<Tour>> GetAllToursAsync(string username)
    {
        try
        {
            var tours = await _dbContext.Tours.AsNoTracking().Where(t => t.User.Username == username).ToListAsync();
            return tours;
        }
        catch (Exception e) when(e is DbUpdateException || e is ArgumentException)
        {
            return null;
        }
    }

    public async Task<Tour?> GetTourByIdAsync(string username, int tourId)
    {
        return await _dbContext.Tours.SingleOrDefaultAsync(t => t.Id == tourId && t.User.Username == username);
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
            throw new DuplicateKeyException($"Tour with name '{tour.Name}' already exists for user '{username}'.", e);
        }
    }

    public async Task UpdateTourAsync(string username, Tour tour)
    {
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
            return false;
        }
        
        _dbContext.Tours.Remove(tour);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}