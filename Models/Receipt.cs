namespace StoreControlAPI.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CashierId { get; set; } 
        public decimal TotalAmount { get; set; }

        public Receipt()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
