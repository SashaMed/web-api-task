using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class FridgeModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Model name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        [Required]
        public int Year { get; set; }   

    }
}
