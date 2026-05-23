namespace UserOrderAPI.DTOs
{
    public class UserWithOrdersResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<OrderResponseDto>? Orders { get; set; }
    }
}
