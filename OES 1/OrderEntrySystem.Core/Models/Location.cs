using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrderEntrySystem.Core.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string City { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "varchar(100)")]
        public string State { get; set; } = string.Empty;
    }
}
