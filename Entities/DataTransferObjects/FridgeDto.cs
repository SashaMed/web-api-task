using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class FridgeDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public Guid FridgeModelId { get; set; }

        public FridgeModel FridgeModel { get; set; }    

        public IEnumerable<ProductDto> Products { get; set; }
    }
}
