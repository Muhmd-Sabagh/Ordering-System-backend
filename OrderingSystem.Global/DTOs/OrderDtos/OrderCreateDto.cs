using System.ComponentModel.DataAnnotations;

namespace OrderingSystem.Global.DTOs.OrderDtos
{
    public class OrderCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Details { get; set; } = string.Empty;
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }
    }
}
