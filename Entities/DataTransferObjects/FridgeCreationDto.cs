using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class FridgeCreationDto
    {
        [Required(ErrorMessage = "Fridge name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Name is 30 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Owner name is a required field.")]
        [MaxLength(30, ErrorMessage = "Maximum length for the Owner name is 30 characters.")]
        public string OwnerName { get; set; }

        [Required(ErrorMessage = "Fridge ModelId is a required field.")]
        public Guid FridgeModelId { get; set; }
    }
}
