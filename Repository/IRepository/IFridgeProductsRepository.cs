using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IFridgeProductsRepository
    {
        Task<IEnumerable<ProductDto>> GetFridgeProductsAsync(Guid fridgeId);
        Task<IEnumerable<ProductDto>> GetFridgeProductsAsync(Guid fridgeId, RequestParameters pagingPrameters);
        void CreateFridgeProduct(Guid productId, Guid fridgeId, int quantity);

        void DeleteFridgeProducts(Guid fridgeId);

        void DeleteProductFromFridge(Guid productId, Guid fridgeId);

        void DeleteProduct(Guid productId);

        Task<int> GetFridgeProductsCountAsync(Guid fridgeIid);


        Task<IEnumerable<Product>> GetProductsNotFromFridge(Guid fridgeIid, RequestParameters parameters);
        Task<int> GetProductsNotFromFridgeCount(Guid fridgeIid);

    }
}
