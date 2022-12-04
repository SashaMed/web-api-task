using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Fridge
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Fridge name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fridge owner name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the Name is 60 characters.")]
        public string OwnerName { get; set; }

        [ForeignKey(nameof(FridgeModel))]
        public Guid FridgeModelId { get; set; }
        public FridgeModel FridgeModel { get; set; }

    }
}
