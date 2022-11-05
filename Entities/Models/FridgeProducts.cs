using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FridgeProducts
    {
        [Key]
        public Guid Id { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("Fridges")]
        public Guid FridgeId { get; set; }
        public Fridge Fridge { get; set; }

        [ForeignKey("Products")]
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
    }
}
