using Contracts.IRepository;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    internal class ProductsRepository : RepositoryBase<Product>, IProductsRepository
    {
        public ProductsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateProduct(Product product)
        {
            product.Id = Guid.NewGuid();
            Create(product);
        }

        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public IEnumerable<Product> GetAllProducts(bool trackChanges)
        {
            return FindAll(trackChanges).OrderBy(c => c.Name).ToList();
        }

        public Product GetProduct(Guid productId, bool trackChanges)
        {
            return FindByCondition(b => b.Id == productId, trackChanges).SingleOrDefault();
        }
    }
}
