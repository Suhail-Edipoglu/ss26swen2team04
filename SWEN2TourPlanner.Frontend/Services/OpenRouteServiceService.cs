using SWEN2TourPlanner.Frontend.Services.Interfaces;

namespace SWEN2TourPlanner.Frontend.Services;

public class OpenRouteServiceService(HttpClient httpClient) : IRouteApiService {
    private readonly HttpClient _httpClient = httpClient;

}