using System.ComponentModel.DataAnnotations.Schema;

namespace OrderingSystem.Global.Entities
{
    public class Order : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Details { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Customer))]
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
    }
}
