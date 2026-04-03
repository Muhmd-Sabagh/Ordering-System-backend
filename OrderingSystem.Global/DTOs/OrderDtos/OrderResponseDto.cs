namespace OrderingSystem.Global.DTOs.OrderDtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
