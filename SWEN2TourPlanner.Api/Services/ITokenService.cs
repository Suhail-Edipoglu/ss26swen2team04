using SWEN2TourPlanner.Models;

namespace SWEN2TourPlanner.Api.Services;

public interface ITokenService
{
    string GenerateToken(User user);
}