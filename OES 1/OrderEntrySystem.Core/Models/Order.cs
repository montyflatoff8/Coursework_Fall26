using OrderEntrySystem.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OrderEntrySystem.Core.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }
    }
}
