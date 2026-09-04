using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderEntrySystem.Core
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(250)")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "Decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
    }
}
