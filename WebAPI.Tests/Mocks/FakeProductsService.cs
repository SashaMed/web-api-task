using Moq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Mocks
{
	internal class FakeProductsService
	{
		public Mock<IProductsService> Mock;
		public IProductsService Service;


		public FakeProductsService()
		{
			Mock = new Mock<IProductsService>();

			Service = Mock.Object;
		}
	}
}
