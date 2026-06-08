namespace SWEN2TourPlanner.Models;

public class User
{
    public User()
    {
    }

    public User(int id, string username, string password, List<Tour>? tours) : this()
    {
        Id = id;
        Username = username;
        HashedPassword = password;
        Tours = tours;
    }

    public int Id { get; set; }
    public required string Username { get; set; }
    public required string HashedPassword { get; set; }
    public List<Tour>? Tours { get; set; }
}