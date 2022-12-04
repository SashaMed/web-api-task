using Moq;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Mocks
{
    public class FakeLogger
    {
        public Mock<ILoggerManager> Mock;
        public ILoggerManager Logger;


        public FakeLogger()
        {
            Mock = new Mock<ILoggerManager>();

            Logger = Mock.Object;
        }
    }
}
