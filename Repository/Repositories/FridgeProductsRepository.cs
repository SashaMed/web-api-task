using Contracts.IRepository;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.EntityFrameworkCore;
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

        public void CreateFridgeProduct(Guid productId, Guid fridgeId, int quantity)
        {
            var link = new FridgeProducts
            {
                Id = Guid.NewGuid(),
                FridgeId = fridgeId,
                ProductId = productId,
                Quantity = quantity
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

        public void DeleteProduct(Guid productId)
        {
            var links = Context.FridgeProducts.Where(b => b.ProductId == productId).ToList();
            foreach (var link in links)
            {
                Delete(link);
            }

        }

        public void DeleteProductFromFridge(Guid productId, Guid fridgeId)
        {
            var link = Context.FridgeProducts.Where(p => p.ProductId == productId & p.FridgeId == fridgeId).FirstOrDefault();
            if (link != null)
                Delete(link);
        }

        public async Task<IEnumerable<ProductDto>> GetFridgeProductsAsync(Guid fridgeId, RequestParameters pagingPrameters)
        {
            var links = await Context.FridgeProducts.Where(b => b.FridgeId == fridgeId).Join(Context.Products,
                c => c.ProductId,
                p => p.Id,
                (c,p) => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    DefaultQuantity = p.DefaultQuantity,
                    Quantity = c.Quantity
                    
                })
                .Skip((pagingPrameters.PageNumber - 1) * pagingPrameters.PageSize)
                .Take(pagingPrameters.PageSize)
                .ToListAsync();

            return links;
        }

        public Task<int> GetFridgeProductsCountAsync(Guid fridgeIid)
        {
            return Context.FridgeProducts.Where(b => b.FridgeId == fridgeIid).CountAsync();
        }
    }
}
