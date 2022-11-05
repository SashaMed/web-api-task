using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IFridgeProductsRepository
    {
        IEnumerable<Product> GetFridgeProducts(Guid fridgeId);
        void CreateFridgeProduct(Guid productId, Guid fridgeId);

        void DeleteFridgeProducts(Guid fridgeId);
    }
}
