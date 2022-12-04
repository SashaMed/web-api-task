using Entities;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
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

        public async Task<IEnumerable<Product>> GetAllProductsAsync(RequestParameters parameters, bool trackChanges)
        {
            return await FindAll(trackChanges)
                .OrderBy(c => c.Name)
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();
        }

        public async Task<Product> GetProductAsync(Guid productId, bool trackChanges)
        {
            return await FindByCondition(b => b.Id == productId, trackChanges).SingleOrDefaultAsync();
        }

        public async Task<int> GetProductCountAsync()
        {
            return await Context.Products.CountAsync();
        }
    }
}
