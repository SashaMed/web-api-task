using Entities.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Responces
{
    public class GetProductsNotInFridgeResponce
    {
        public FridgeDto Fridge { get; set; }
        public int ProductsCount { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
