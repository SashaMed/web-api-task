﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Entities.Responces;
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
    public class ProductsControllerTests
    {
        private IMapper _mapper;

		private RequestParameters requestParams = new RequestParameters
		{
			PageNumber = 1,
			PageSize = 10
		};

		public ProductsControllerTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            _mapper = mappingConfig.CreateMapper();
        }


        [Fact]
        public async Task GetAllProducts_ShouldReturnOk_WithData()
        {
            var fakeService = new FakeProductsService();
            fakeService.Mock.Setup(s => s.GetProducts(requestParams))
                .Returns(Task.FromResult(new Entities.Responces.GetAllProductsResponce
                {
                    Products = FakeData.ProductsDto,
                    ProductsCount = FakeData.ProductsDto.Count()
                }));
            var controller = new ProductsController(null, null, _mapper,fakeService.Service);


            var response = await controller.GetProducts(requestParams);


            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            var responseProducts = okResult.Value as GetAllProductsResponce;
            Assert.NotNull(responseProducts);
			Assert.Equal(FakeData.Products.Count(), responseProducts.ProductsCount);
		}

		[Fact]
        public async Task GetAllProducts_ShouldReturnOk_NoData()
        {
			var fakeService = new FakeProductsService();
			fakeService.Mock.Setup(s => s.GetProducts(requestParams))
				.Returns(Task.FromResult(new Entities.Responces.GetAllProductsResponce
				{
					Products = new List<ProductDto>(),
					ProductsCount = 0
				}));
			var controller = new ProductsController(null, null, _mapper,fakeService.Service);


            var response = await controller.GetProducts(requestParams);


            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            var responseProducts = okResult.Value as GetAllProductsResponce;
            Assert.NotNull(responseProducts);
            Assert.Equal(0, responseProducts.Products.Count());
        }


        [Fact]
        public async Task GetSingleProductById_ShouldReturnOk_WithData()
        {
            var guid = new Guid("b2b56b4f-d066-45a6-a7e8-fa34e6cf1772");
            FakeRepository fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Products.GetProductAsync(guid, false))
                .Returns(Task.FromResult(FakeData.Products.Where(b => b.Id == guid).FirstOrDefault()));
            var controller = new ProductsController(fakeRepo.Repository, null, _mapper, null);


            var response = await controller.GetSingleProductById(guid);


            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            var responseProduct = okResult.Value as ProductDto;
            Assert.NotNull(responseProduct);
            Assert.Equal(responseProduct.Id, guid);
        }


        [Fact]
        public async Task GetAllProducts_ShouldReturnOk_PagingTest()
        {
			List<ProductDto> products = (List<ProductDto>)FakeData.ProductsDto;
			var fakeService = new FakeProductsService();
			fakeService.Mock.Setup(s => s.GetProducts(requestParams))
				.Returns(Task.FromResult(new Entities.Responces.GetAllProductsResponce
				{
					Products = (IEnumerable<ProductDto>)products.GetRange(0, 2),
					ProductsCount = 2
				}));
			var controller = new ProductsController(null, null, _mapper, fakeService.Service);


            var response = await controller.GetProducts(requestParams);


            Assert.NotNull(response);
            Assert.IsType<OkObjectResult>(response);
            var okResult = response as OkObjectResult;
            Assert.Equal(200, okResult.StatusCode);
            var responseProducts = okResult.Value as GetAllProductsResponce;
            Assert.NotNull(responseProducts);
            Assert.Equal(2, responseProducts.Products.Count());
        }


        [Fact]
        public async Task GetSingleProductById_ShouldReturnNotFound()
        {
            var guid = Guid.NewGuid();
            FakeRepository fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Products.GetProductAsync(guid, false))
                .Returns(Task.FromResult((Product)null));
            var fakeLogger = new FakeLogger();
            var controller = new ProductsController(fakeRepo.Repository, fakeLogger.Logger, _mapper, null);


            var response = await controller.GetSingleProductById(guid);


            Assert.NotNull(response);
            Assert.IsType<NotFoundResult>(response);
            var result = response as NotFoundResult;
            Assert.Equal(404, result.StatusCode);
        }


        [Fact]
        public async Task DeleteProduct_ShouldReturnNoContent()
        {
            var guid = new Guid("0d071192-0ce9-4063-9829-a90cd76dbb2b");
            var product = FakeData.Products.Where(c => c.Id == guid).FirstOrDefault();
            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Products.GetProductAsync(guid, false)).Returns(Task.FromResult(product));
            var fakeService = new FakeProductsService();
            fakeService.Mock.Setup(s => s.DeleteProduct(guid));
            var fakeLogger = new FakeLogger();
            var controller = new ProductsController(fakeRepo.Repository, fakeLogger.Logger, _mapper, fakeService.Service);
            
            
            var response = await controller.DeleteProduct(guid);


            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response);
            var notFoundResult = response as NoContentResult;
            Assert.Equal(204, notFoundResult.StatusCode);
        }


        [Fact]
        public async Task CreateProduct_ShouldReturnCreatedAtRoute()
        {
            var fridgeId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203");
			var request = new ProductCreationDto
            {
                Name = "product",
                Description = "description",
                DefaultQuantity = 10
            };
			var fakeService = new FakeProductsService();
			fakeService.Mock.Setup(s => s.CreateProduct(fridgeId, request))
                .Returns(Task.FromResult(new ProductDto
                {
                    Name = request.Name,
                    Description = request.Description,
                    DefaultQuantity = request.DefaultQuantity,
                }));
            var controller = new ProductsController(null, null, _mapper, fakeService.Service);


            var response = await controller.CreateProduct(fridgeId, request);


            Assert.NotNull(response);
            Assert.IsType<CreatedAtRouteResult>(response);
            var result = response as CreatedAtRouteResult;
            Assert.Equal(201, result.StatusCode);
            var resultVal = result.Value as ProductDto;
            Assert.Equal(resultVal.Name, request.Name);
            Assert.Equal(resultVal.Description, request.Description);
            Assert.Equal(resultVal.DefaultQuantity, request.DefaultQuantity);
        }


        [Fact]
        public async Task UpdateProduct_ShouldReturnNoContent()
        {
            var guid = new Guid("b5b47b15-709e-442a-bcf1-320e8543ff2b");
            var product = FakeData.Products.Where(b => b.Id == guid).FirstOrDefault();
            var fakeRepo = new FakeRepository();
            fakeRepo.Mock.Setup(s => s.Products.GetProductAsync(guid, true)).Returns(Task.FromResult(product));
            var request = new ProductUpdateDto
            {
                Name = "product",
                Description = "description",
                DefaultQuantity = 10
            };
			var fakeService = new FakeProductsService();
            fakeService.Mock.Setup(s => s.UpdateProduct(guid, request));
			var fakeLogger = new FakeLogger();
            var controller = new ProductsController(fakeRepo.Repository, fakeLogger.Logger, _mapper, fakeService.Service);
            
            
            var response = await controller.UpdateProduct(guid, request);


            Assert.NotNull(response);
            Assert.IsType<NoContentResult>(response);
            var noContent = response as NoContentResult;
            Assert.Equal(204, noContent.StatusCode);
        }
    }
}
