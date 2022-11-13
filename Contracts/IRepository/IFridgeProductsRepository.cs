using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.IRepository
{
    public interface IFridgeProductsRepository
    {
        Task<IEnumerable<ProductDto>> GetFridgeProductsAsync(Guid fridgeId, RequestParameters pagingPrameters);
        void CreateFridgeProduct(Guid productId, Guid fridgeId, int quantity);

        void DeleteFridgeProducts(Guid fridgeId);

        void DeleteProductFromFridge(Guid productId, Guid fridgeId);

        void DeleteProduct(Guid productId);

        Task<int> GetFridgeProductsCountAsync(Guid fridgeIid);
    }
}
