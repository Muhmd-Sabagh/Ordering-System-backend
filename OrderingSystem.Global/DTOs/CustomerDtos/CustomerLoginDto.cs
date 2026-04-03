using System.ComponentModel.DataAnnotations;

namespace OrderingSystem.Global.DTOs.CustomerDtos
{
    public class CustomerLoginDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
