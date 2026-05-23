using System.ComponentModel.DataAnnotations;

namespace UserOrderAPI.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MinLength(2)]
        [MaxLength(10)]
        public string Name { get; set; } = string.Empty;
    }
}
