using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Requests;
using Entities.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IFridgeService
    {
        Task<GetFridgeDetailsResponce> GetFridge(Guid id, RequestParameters pagingPrameters);
        Task DeleteFridge(Guid id, Fridge fridge);
        Task<GetProductsNotInFridgeResponce> GetProductsNotInFridge(Guid id, RequestParameters pagingPrameters);
        Task AddProductsToFridge(Guid id, AddProductsRequest request);
        Task<IEnumerable<FridgeDto>> GetFridges();

        Task<IEnumerable<ProductDto>> GetProductsByFridgeId(Guid id, RequestParameters pagingPrameters);

        Task<FridgeDto> CreateFridge(FridgeCreationDto fridge);

        Task DeleteProductFromFridge(Guid id, Guid productId);
    }
}
