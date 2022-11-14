using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        public string ImagePath { get; set; }
        public string Description { get; set; }
        public int DefaultQuantity { get; set; }

        public override bool Equals(object? obj)
        {
            Product product = obj as Product;
            var n = product.Name;
            if (product != null)
            return (Id.CompareTo(product.Id) == 0);
            return false;
        }
    }
}
