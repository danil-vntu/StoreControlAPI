namespace StoreControlAPI.DTOs
{
    public class ResultDto
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public int? UserId { get; set; }
    }
}
