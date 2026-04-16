namespace SWEN2TourPlanner.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Tour>? Tours { get; set; }

    public User()
    {
    }
    
    public User(int  id, string username, string password, List<Tour>? tours) : this()
    {
        Id = id;
        Username = username;
        Password = password;
        Tours = tours;
    }
}