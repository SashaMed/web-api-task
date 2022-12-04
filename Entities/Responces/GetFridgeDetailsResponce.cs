using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responces
{
    public class GetFridgeDetailsResponce
    {
        public int ProductsCount { get; set; }

        public FridgeDto Fridge { get; set; }

        public IEnumerable<ProductDto> Products { get; set; }
    }
}
