using SWEN2TourPlanner.Bll.Exceptions;
using SWEN2TourPlanner.Dal;
using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Bll;

public class TourService : ITourService
{
    private readonly ITourRepository _tourRepository;
    private readonly ILogRepository _logRepository;
    
    public TourService(ITourRepository tourRepository, ILogRepository logRepository)
    {
        _tourRepository = tourRepository;
        _logRepository = logRepository;
    }
    
    public async Task<Tour> GetTourAsync(string username, int tourId)
    {
        return await _tourRepository.GetTourByIdAsync(username, tourId) ?? throw new TourNotFoundException();
    }

    public async Task<List<Tour>> GetToursAsync(string username)
    {
        return await _tourRepository.GetAllToursAsync(username) ?? throw new TourNotFoundException();
    }

    public async Task<Tour> CreateTourAsync(Tour tour, string username)
    {
        try
        {
            await _tourRepository.InsertTourAsync(username, tour);
            return tour; // maybe return tour from db?
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to create tour for user '{username}': {e.Message}", e);
        }
    }

    public async Task<bool> UpdateTourAsync(int tourId, Tour tour)
    {
        try
        {
            tour.Id = tourId;
            await _tourRepository.UpdateTourAsync(tour.User.Username, tour);
            return true;
        }
        catch (TourNotFoundException)
        {
            return false;
        }
    }

    public async Task<bool> RemoveTourAsync(string username, int tourId)
    {
        try
        {
            await _tourRepository.DeleteTourAsync(username, tourId);
            return true;
        }
        catch (TourNotFoundException)
        {
            return false;
        }
    }

    public async Task<List<Tour>> FindMatchingToursAsync(string username, string? searchText = null)
    {
        var tours = await _tourRepository.GetAllToursAsync(username);
        
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return tours;
        }
        
        return tours.Where(t => t.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.From.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.To.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
    }

    public async Task<List<Tour>> ExportToursAsync(string username)
    {
        var tours = await _tourRepository.ExportToursAsync(username);
        
        if (tours == null || tours.Count == 0)
        {
            throw new TourNotFoundException($"No tours found for user '{username}'.");
        }

        int userId = 0;
        int tourId = 0;
        
        foreach (var tour in tours)
        {
            tour.UserId = userId;
            tour.Id = tourId;
            int logId = 0;
            
            if (tour.Logs != null && tour.Logs.Count > 0)
            {
                foreach (var log in tour.Logs)
                {
                    log.TourId = tourId;
                    logId = log.Id;
                    logId++;
                    log.Tour = null;
                }
            }

            tourId++;
            tour.User = null;
        }
        
        return tours;
    }

    public async Task<bool> ImportToursAsync(string username, List<Tour> tours)
    {
        var importResult = await _tourRepository.ImportToursAsync(username, tours);
        return importResult;
    }
}