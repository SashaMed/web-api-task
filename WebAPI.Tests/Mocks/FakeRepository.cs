using Contracts.IRepository;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Mocks
{
    internal class FakeRepository
    {
        public Mock<IRepositoryManager> Mock;
        public IRepositoryManager Repository;


        public FakeRepository()
        {
            Mock = new Mock<IRepositoryManager>();

            Repository = Mock.Object;
        }
    }
}
