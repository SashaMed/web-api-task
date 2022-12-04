using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Requests
{
    public class AddProductsRequest
    {
        public IEnumerable<Guid> Guids { get; set; }
        public IEnumerable<int> QuantityList { get; set; }
    }
}
