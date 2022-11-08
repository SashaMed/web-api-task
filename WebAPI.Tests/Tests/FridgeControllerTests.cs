using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Controllers;
using WebAPI.Tests.Mocks;
using WebAPI.Utils;

namespace WebAPI.Tests.Tests
{
    public class FridgeControllerTests 
    {
        private IMapper _mapper;


        public FridgeControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }



        [Fact]
        public async Task GetAllFridges_ShouldReturnOk_WithData()
        {
            IEnumerable<Fridge> fridges = FakeData.Fridges;

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetAllFridgesAsync(false))
                .Returns(Task.FromResult(fridges));

            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);
            var response = await controller.GetFridges();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);

            var responseFridges = okResult.Value as IEnumerable<FridgeDto>;

            Assert.NotNull(responseFridges);
            Assert.Equal(responseFridges.Count(), fridges.Count());
        }

        [Fact]
        public async Task GetAllFridges_ShouldReturnOk_NoData()
        {
            IEnumerable<Fridge> fridges = new List<Fridge>();

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetAllFridgesAsync(false))
                .Returns(Task.FromResult(fridges));

            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);
            var response = await controller.GetFridges();

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);

            var responseFridges = okResult.Value as IEnumerable<FridgeDto>;

            Assert.NotNull(responseFridges);
            Assert.Equal(responseFridges.Count(), fridges.Count());
        }

        [Fact]
        public async Task GetOneFridge_ShouldReturnOk_WithData()
        {
            var guid = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8");
            var fridge = FakeData.Fridges.Where(c => c.Id == guid).FirstOrDefault();

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false))
                .Returns( Task.FromResult(fridge));
            fakeRepo.Mock.Setup(s => s.FridgeModel.GetFridgeModelAsync(FakeData.FridgeModelVal.Id, false))
                .Returns(Task.FromResult(FakeData.FridgeModelVal));

            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);
            var response = await controller.GetFridge(guid);

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);

            var responseFridge = okResult.Value as FridgeDto;

            Assert.NotNull(responseFridge);
            Assert.Equal(responseFridge.Id, fridge.Id);
        }

        [Fact]
        public async Task GetOneFridge_ShouldReturnNotFound_NoData()
        {
            var guid = Guid.NewGuid();
            var fridge = FakeData.Fridges.Where(c => c.Id == guid).FirstOrDefault();

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false))
                .Returns(Task.FromResult((Fridge)null));
            var fakeLog = new FakeLogger();

            var controller = new FridgeController(fakeRepo.Repository, fakeLog.Logger, _mapper);
            var response = await controller.GetFridge(guid);

            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);

            var notFoundResult = response as NotFoundResult;

            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task CreateFridge_ShouldReturnCreatedAtRoute()
        {
            var fridge = new Fridge();
            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.CreateFridge(fridge));

            var request = new FridgeCreationDto
            {
                Name = "123",
                OwnerName = "me",
                FridgeModelId = Guid.NewGuid(),
            };
            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);
            var response = await controller.CreateFridge(request);

            Assert.NotNull(response);
            Assert.IsType<CreatedAtRouteResult>(response);

            var result = response as CreatedAtRouteResult;

            Assert.Equal(201, result.StatusCode);
            var resultVal = result.Value as FridgeDto;
            Assert.Equal(resultVal.Name, request.Name);
            Assert.Equal(resultVal.OwnerName, request.OwnerName);
            Assert.Equal(resultVal.FridgeModelId, request.FridgeModelId);
        }


        [Fact]
        public async Task UpdateFridge_ShouldReturnNoContent()
        {
            var guid = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8");
            var fridge = FakeData.Fridges.Where(c => c.Id == guid).FirstOrDefault();
            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, true)).Returns(Task.FromResult(fridge));

            var request = new FridgeCreationDto
            {
                Name = "123",
                OwnerName = "me",
                FridgeModelId = Guid.NewGuid(),
            };
            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);
            var response = await controller.UpdateFridge(guid, request);

            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response);

            var noContent = response as NoContentResult;

            Assert.Equal(204, noContent.StatusCode);
        }

        [Fact]
        public async Task UpdateFridge_ShouldReturnNotFound()
        {
            var guid = Guid.NewGuid();
            var fridge = FakeData.Fridges.Where(c => c.Id == guid).FirstOrDefault();

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false))
                .Returns(Task.FromResult((Fridge)null));
            var fakeLog = new FakeLogger();

            var request = new FridgeCreationDto();
            var controller = new FridgeController(fakeRepo.Repository, fakeLog.Logger, _mapper);
            var response = await controller.UpdateFridge(guid, request);

            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);

            var notFoundResult = response as NotFoundResult;

            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async Task GetProductsByFridgeId_ShouldReturnOk_WithData()
        {
            var guid = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8");
            var fridge = FakeData.Fridges.First(c => c.Id == guid);
            var requestParams = new RequestParameters
            {
                PageNumber = 1,
                PageSize = 10
            };
            List<Product> products = (List<Product>)FakeData.Products;
            FakeRepository fakeRepo = new FakeRepository();

            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false))
                .Returns(Task.FromResult(fridge));
            fakeRepo.Mock.Setup(s => s.FridgeProducts.GetFridgeProductsAsync(guid, requestParams))
                .Returns(Task.FromResult(FakeData.Products.Where(b => b.DefaultQuantity == 10)));


            var controller = new FridgeController(fakeRepo.Repository, null, _mapper);

            var response = await controller.GetProductsByFridgeId(guid, requestParams);

            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);

            var okResult = response as OkObjectResult;

            Assert.Equal(200, okResult.StatusCode);

            var responseProducts = okResult.Value as IEnumerable<ProductDto>;

            Assert.NotNull(responseProducts);
            Assert.Equal(responseProducts.Count(), 2);
        }

        [Fact]
        public async Task DeleteFridge_ShouldReturnNoContent()
        {
            var guid = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8");
            var fridge = FakeData.Fridges.Where(c => c.Id == guid).FirstOrDefault();
            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false)).Returns(Task.FromResult(fridge));
            fakeRepo.Mock.Setup(s => s.Fridge.DeleteFridge(fridge));
            fakeRepo.Mock.Setup(s => s.FridgeProducts.DeleteFridgeProducts(guid));

            var fakeLogger = new FakeLogger();
            var request = new FridgeCreationDto();
            var controller = new FridgeController(fakeRepo.Repository, fakeLogger.Logger, _mapper);
            var response = await controller.DeleteFridge(guid);

            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response);

            var notFoundResult = response as NoContentResult;

            Assert.Equal(204, notFoundResult.StatusCode);
        }

    }
}
