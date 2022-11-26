using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Responces;
using Repository.IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ProductsService
{
    public class ProductsService : IProductsService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public ProductsService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task<ProductDto> CreateProduct(Guid fridgeId, ProductCreationDto product)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(fridgeId, false);
            var productEntity = _mapper.Map<Product>(product);
            _repositoryManager.Products.CreateProduct(productEntity);
            _repositoryManager.FridgeProducts.CreateFridgeProduct(productEntity.Id, fridgeId, product.Quantity);
            await _repositoryManager.SaveAsync();
            var toReturn = _mapper.Map<ProductDto>(productEntity);
            return toReturn;
        }

        public async Task DeleteProduct(Guid id)
        {
            var product = await _repositoryManager.Products.GetProductAsync(id, false);
            _repositoryManager.Products.DeleteProduct(product);
            _repositoryManager.FridgeProducts.DeleteProduct(id);
            await _repositoryManager.SaveAsync();
        }

        public async Task<GetAllProductsResponce> GetProducts(RequestParameters pagingPrameters)
        {
            var products = await _repositoryManager.Products.GetAllProductsAsync(pagingPrameters, false);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            var productCount = await _repositoryManager.Products.GetProductCountAsync();
            var responce = new GetAllProductsResponce
            {
                Products = productsDto,
                ProductsCount = productCount
            };
            return responce;
        }

        public async Task UpdateProduct(Guid id, ProductUpdateDto product)
        {
            var productEntity = await _repositoryManager.Products.GetProductAsync(id, true);
            _mapper.Map(product, productEntity);
            await _repositoryManager.SaveAsync();
        }
    }
}
