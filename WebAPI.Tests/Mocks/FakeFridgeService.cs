using Moq;
using Repository.IRepository;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Mocks
{
	internal class FakeFridgeService
	{
		public Mock<IFridgeService> Mock;
		public IFridgeService Service;


		public FakeFridgeService()
		{
			Mock = new Mock<IFridgeService>();

			Service = Mock.Object;
		}
	}
}
