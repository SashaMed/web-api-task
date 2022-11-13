using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class ProductCreationDto
    {
        [Required(ErrorMessage = "Product name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Default Quantity name is a required field.")]
        [Range(0, int.MaxValue, ErrorMessage = "DefaultQuantity is required and it can't be lower than 0")]
        public int DefaultQuantity { get; set; }

        public int Quantity { get; set; }
    }
}
