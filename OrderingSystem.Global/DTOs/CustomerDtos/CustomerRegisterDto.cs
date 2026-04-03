using System.ComponentModel.DataAnnotations;

namespace OrderingSystem.Global.DTOs.CustomerDtos
{
    public class CustomerRegisterDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string? LastName { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Username must be at least 3 characters.")]
        [MaxLength(20, ErrorMessage = "Username must not exceed  20 characters.")]
        public string Username { get; set; } = string.Empty;

        [Required]
        [MinLength(3, ErrorMessage = "Password must be at least 6 characters.")]
        [MaxLength(20, ErrorMessage = "Password must not exceed 20 characters.")]
        public string Password { get; set; } = string.Empty;
    }
}
