using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllProductsAsync(RequestParameters parameters, bool trackChanges);

        Task<Product> GetProductAsync(Guid productId, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteProduct(Product product);

        Task<int> GetProductCountAsync();
    }
}
