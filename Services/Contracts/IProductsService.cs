using Entities.DataTransferObjects;
using Entities.RequestFeatures;
using Entities.Responces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Contracts
{
    public interface IProductsService
    {
        Task<GetAllProductsResponce> GetProducts(RequestParameters pagingPrameters);

        Task<ProductDto> CreateProduct(Guid fridgeId, ProductCreationDto product);

        Task DeleteProduct(Guid id);

        Task UpdateProduct(Guid id, ProductUpdateDto product);
    }
}
