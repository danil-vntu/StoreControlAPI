using StoreControlAPI.Enums;
namespace StoreControlAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public required Roles Role { get; set; } 
    }
}
