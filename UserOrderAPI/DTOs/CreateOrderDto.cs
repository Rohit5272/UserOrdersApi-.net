namespace UserOrderAPI.DTOs
{
    public class CreateOrderDto
    {
        public string ProductName { get; set; } = string.Empty;

        public int UserId { get; set; } 
    }
}
