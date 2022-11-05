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
    internal class FridgeProductsRepository : RepositoryBase<FridgeProducts>, IFridgeProductsRepository
    {
        public FridgeProductsRepository(RepositoryContext context) : base(context)
        {
        }

        public void CreateFridgeProduct(Guid productId, Guid fridgeId)
        {
            var link = new FridgeProducts
            {
                Id = Guid.NewGuid(),
                FridgeId = fridgeId,
                ProductId = productId
            };
            Create(link);
        }

        public void DeleteFridgeProducts(Guid fridgeId)
        {
            var links = Context.FridgeProducts.Where(p => p.Id == fridgeId).ToList();
            foreach(var link in links)
            {
                Delete(link);
            }
        }

        public IEnumerable<Product> GetFridgeProducts(Guid fridgeId)
        {
            var links = Context.FridgeProducts.Where(b => b.FridgeId == fridgeId).Join(Context.Products,
                c => c.ProductId,
                p => p.Id,
                (c,p) => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DefaultQuantity = p.DefaultQuantity
                });

            return links;
        }
    }
}
