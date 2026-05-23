using System.Text.Json.Serialization;

namespace UserOrderAPI.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ProductName { get; set; }

        public int UserId { get; set; }
        
        [JsonIgnore]
        public User? User { get; set; }
    }
}
