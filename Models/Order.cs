using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SportsStore.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [HttpBindNever]
        public int Id { get; set; }
        [Required]
        public string Customer { get; set; }
        [Required]
        [HttpBindNever]
        public decimal TotalCost { get; set; }
        public ICollection<OrderLine> Lines { get; set; }

    }
}