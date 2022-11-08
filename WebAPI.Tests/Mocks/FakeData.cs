using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Tests.Mocks
{
    internal static class FakeData
    {
        public static IEnumerable<Fridge> Fridges
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

        public static FridgeModel FridgeModelVal
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

        public static IEnumerable<Product> Products
        {
            get
            {
                IEnumerable<Product> products = new List<Product>()
                {
                    new Product()
                    {
                        Id = new Guid("b5b47b15-709e-442a-bcf1-320e8543ff2b"),
                        Name = "milk",
                        Description = "ne sasha",
                        DefaultQuantity = 1
                    },
                    new Product()
                    {
                        Id = new Guid("a6ed0776-5f3c-48ce-a49f-03142c2f1602"),
                        Name = "potato",
                        Description = "10 kg",
                        DefaultQuantity = 100
                    },
                    new Product()
                    {
                        Id = new Guid("0d071192-0ce9-4063-9829-a90cd76dbb2b"),
                        Name = "salo",
                        Description = "ukraine salo",
                        DefaultQuantity = 10
                    },
                    new Product()
                    {
                        Id = new Guid("b2b56b4f-d066-45a6-a7e8-fa34e6cf1772"),
                        Name = "eggs",
                        Description = "c1",
                        DefaultQuantity = 10
                    }
                };

                return products;
            }
        }

        public static IEnumerable<FridgeProducts> FridgeProductsVals
        {
            get
            {
                IEnumerable<FridgeProducts> fridgeProducts = new List<FridgeProducts>()
                {
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("b2b56b4f-d066-45a6-a7e8-fa34e6cf1772"),
                        FridgeId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8")
                    },
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("0d071192-0ce9-4063-9829-a90cd76dbb2b"),
                        FridgeId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8")
                    },
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("a6ed0776-5f3c-48ce-a49f-03142c2f1602"),
                        FridgeId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203")
                    },
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("b5b47b15-709e-442a-bcf1-320e8543ff2b"),
                        FridgeId = new Guid("0b57d31d-3a23-4c83-9483-08dac0156203")
                    }

                };
                return fridgeProducts;
            }
        }
        
    }
}
