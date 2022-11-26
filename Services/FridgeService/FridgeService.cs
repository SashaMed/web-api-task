using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Requests;
using Entities.Responces;
using Repository.IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.FridgeService
{
    public class FridgeService : IFridgeService
    {
        private IRepositoryManager _repositoryManager;
        private IMapper _mapper;

        public FridgeService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task AddProductsToFridge(Guid id, AddProductsRequest request)
        {
            var guids = (List<Guid>)request.Guids;
            var quantityes = (List<int>)request.QuantityList;
            for (int i = 0; i < guids.Count(); i++)
            {
                _repositoryManager.FridgeProducts.CreateFridgeProduct(productId: guids[i], fridgeId: id, quantityes[i]);
            }
            await _repositoryManager.SaveAsync();
        }

        public async Task<FridgeDto> CreateFridge(FridgeCreationDto fridge)
        {
            var fridgeEntity = _mapper.Map<Fridge>(fridge);
            _repositoryManager.Fridge.CreateFridge(fridgeEntity);
            await _repositoryManager.SaveAsync();
            var toReturn = _mapper.Map<FridgeDto>(fridgeEntity);
            return toReturn;
        }

        public async Task DeleteFridge(Guid id, Fridge fridge)
        {
            _repositoryManager.FridgeProducts.DeleteFridgeProducts(id);
            _repositoryManager.Fridge.DeleteFridge(fridge);
            await _repositoryManager.SaveAsync();
        }

        public async Task DeleteProductFromFridge(Guid id, Guid productId)
        {
            _repositoryManager.FridgeProducts.DeleteProductFromFridge(productId, id);
            await _repositoryManager.SaveAsync();
        }

        public async Task<GetFridgeDetailsResponce> GetFridge(Guid id, RequestParameters pagingPrameters)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, false);
            var fridgeModel = await _repositoryManager.FridgeModel.GetFridgeModelAsync(fridge.FridgeModelId, false);
            fridge.FridgeModel = fridgeModel;
            var fridgeDto = new FridgeDto
            {
                Id = fridge.Id,
                Name = fridge.Name,
                FridgeModelId = fridgeModel.Id,
                FridgeModel = fridgeModel,
                OwnerName= fridge.OwnerName,
            };
            var responce = new GetFridgeDetailsResponce
            {
                Fridge = fridgeDto,
                Products = await _repositoryManager.FridgeProducts.GetFridgeProductsAsync(id, pagingPrameters),
                ProductsCount = await _repositoryManager.FridgeProducts.GetFridgeProductsCountAsync(id)
            };
            return responce;
        }

        public async Task<IEnumerable<FridgeDto>> GetFridges()
        {
            var fridges = await _repositoryManager.Fridge.GetAllFridgesWithModels(false);
            var fridgesDto = _mapper.Map<IEnumerable<FridgeDto>>(fridges);
            return fridgesDto;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByFridgeId(Guid id, RequestParameters pagingPrameters)
        {
            var products = await _repositoryManager.FridgeProducts.GetFridgeProductsAsync(id, pagingPrameters);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDto;
        }

        public async Task<GetProductsNotInFridgeResponce> GetProductsNotInFridge(Guid id, RequestParameters pagingPrameters)
        {
            var fridge = await _repositoryManager.Fridge.GetFridgeAsync(id, trackChanges: false);
            var fridgeDto = _mapper.Map<FridgeDto>(fridge);
            var products = await _repositoryManager.FridgeProducts.GetProductsNotFromFridge(id, pagingPrameters);
            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);
            var responce = new GetProductsNotInFridgeResponce
            {
                Products = productsDto,
                Fridge = fridgeDto,
                ProductsCount = await _repositoryManager.FridgeProducts.GetProductsNotFromFridgeCount(id)
            };
            return responce;
        }
    }
}
