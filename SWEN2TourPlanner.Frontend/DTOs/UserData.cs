using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SWEN2TourPlanner.Frontend.DTOs;

public class UserData : ObservableValidator {
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "The {0} must be longer than {2} and shorter than {1} characters.")]
    public string Username { get; set; }
    
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "The {0} must be longer than {2} characters.")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
}