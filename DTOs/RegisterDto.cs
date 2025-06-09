using System.ComponentModel.DataAnnotations;

namespace StoreControlAPI.DTOs
{
    public class RegisterDto
    {
         
        public required string Name {  get; set; }
         
        public required string Username { get; set; }
         
        public required string Password { get; set; }
    }
}
