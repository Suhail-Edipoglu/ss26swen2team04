using System.ComponentModel.DataAnnotations;

namespace SWEN2TourPlanner.Api.Dtos;

public class TokenDto
{
    [Required] 
    public required string Token { get; set; }
}