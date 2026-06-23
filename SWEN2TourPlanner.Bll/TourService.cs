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
            return tour;
        }
        catch (DuplicateKeyException)
        {
            throw new DuplicateKeyException($"Tour with name '{tour.Name}' already exists for user '{username}'.");
        }
    }

    public async Task<bool> UpdateTourAsync(int tourId, Tour tour)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveTourAsync(int tourId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Tour>> FindMatchingToursAsync(string username, string? searchText = null)
    {
        var tours = await _tourRepository.GetAllToursAsync(username);
        
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return tours;
        }
        
        return tours.Where(t => t.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.From.Contains(searchText, StringComparison.OrdinalIgnoreCase) ||
                        t.To.Contains(searchText, StringComparison.OrdinalIgnoreCase));
    }
}