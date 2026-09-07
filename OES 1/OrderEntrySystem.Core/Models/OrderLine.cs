using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace OrderEntrySystem.Core.Models
{
    public class OrderLine
    {
        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [JsonIgnore] // Prevents circular reference during JSON serialization
        public Order? Order { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product? Product { get; set; }
    }
}
