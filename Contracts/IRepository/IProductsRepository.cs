using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAllProducts(bool trackChanges);

        Product GetProduct(Guid productId, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);
    }
}
