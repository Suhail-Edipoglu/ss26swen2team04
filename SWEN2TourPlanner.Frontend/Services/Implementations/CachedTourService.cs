using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Frontend.Services.Implementations;

public class CachedTourService : ITourService
{
    private readonly ILogService _logService;
    
    private List<Tour>? _tours;

    public CachedTourService(ILogService logService)
    {
        _logService = logService;
        _tours = new List<Tour>()
        {
            new Tour(0, "TestTour1", "Das ist die erste Test Tour", "Wien", 
                "Linz", "OEBB Zug", 184, 80,
                "Hier stehen die Routeninformationen", 0, _logService.GetLogs(0)),
            new Tour(1, "TestTour2", "Das ist die zweite Test Tour", "Linz", 
                "Wien", "OEBB Zug", 184, 80,
                "Hier stehen die Routeninformationen", 0, _logService.GetLogs(1)),
            new Tour(2, "TestTour3", "Das ist die dritte Test Tour", "Wien", 
                "Salzburg", "OEBB Zug", 298, 150,
                "Hier stehen die Routeninformationen", 0, _logService.GetLogs(2)),
        };
    }
    
    public List<Tour>? GetTours(int userId)
    {
        return _tours;
    }

    public void CreateTour(Tour tour)
    {
        throw new NotImplementedException();
    }

    public void UpdateTour(Tour tour)
    {
        throw new NotImplementedException();
    }

    public void DeleteTour(int tourId)
    {
        _tours?.RemoveAll(t => t.Id == tourId);
    }
}