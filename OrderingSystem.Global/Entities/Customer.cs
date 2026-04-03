using System.ComponentModel.DataAnnotations;

namespace OrderingSystem.Global.Entities
{
    public class Customer : BaseEntity
    {
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string Address { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Username { get; set; } = string.Empty;

        [MaxLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? BannedUntil { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
