namespace OrderingSystem.Global.DTOs.CustomerDtos
{
    public class CustomerResponseDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
        public string? Address { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? BannedUntil { get; set; }
    }
}
