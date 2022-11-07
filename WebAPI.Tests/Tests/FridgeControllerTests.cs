using AutoMapper;
using Contracts;
using Contracts.IRepository;
using Entities.DataTransferObjects;
using Entities.Models;
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

        public IEnumerable<Fridge> Fridges
        {
            get
            {
                IEnumerable<Fridge> fridges = new List<Fridge>()
                {
                    new Fridge()
                    {
                        Id = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"),
                        Name = "123",
                        OwnerName = "ne sasha",
                        FridgeModelId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    },
                    new Fridge()
                    {
                        Id = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203"),
                        Name = "Atlant",
                        OwnerName = "sasha",
                        FridgeModelId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    }
                };
                return fridges;
            }
        }

        public FridgeModel FridgeModelVal
        {
            get
            {
                return new FridgeModel
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "atlant",
                    Year = 1999
                };
            }
        }

        [Fact]
        public async Task GetAllFridges_ShouldReturnOk_WithData()
        {
            IEnumerable<Fridge> fridges = Fridges;

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
            Assert.Equal(responseFridges.Count(), Fridges.Count());
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
            var fridge = Fridges.Where(c => c.Id == guid).FirstOrDefault();

            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Fridge.GetFridgeAsync(guid, false))
                .Returns( Task.FromResult(fridge));
            fakeRepo.Mock.Setup(s => s.FridgeModel.GetFridgeModelAsync(FridgeModelVal.Id, false))
                .Returns(Task.FromResult(FridgeModelVal));

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
            var fridge = Fridges.Where(c => c.Id == guid).FirstOrDefault();

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
            var fridge = Fridges.Where(c => c.Id == guid).FirstOrDefault();
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
            var fridge = Fridges.Where(c => c.Id == guid).FirstOrDefault();

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
    }
}
