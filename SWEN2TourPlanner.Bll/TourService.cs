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
    
    public Task<Tour?> GetTourAsync(int tourId)
    {
        throw new NotImplementedException();
    }

    public Task<Tour?> GetToursAsync(string username)
    {
        throw new NotImplementedException();
    }

    public Task<Tour> CreateTourAsync(Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTourAsync(int tourId, Tour tour)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveTourAsync(int tourId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Tour>> FindMatchingToursAsync(string username, string? searchText = null)
    {
        throw new NotImplementedException();
    }
}