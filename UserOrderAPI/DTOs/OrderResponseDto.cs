namespace UserOrderAPI.DTOs
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int UserId { get; set; }

        public UserResponseDto? User { get; set; }
    }
}
