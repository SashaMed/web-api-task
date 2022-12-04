using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DbConfiguration
{
    internal class FridgeProductsConfiguration : IEntityTypeConfiguration<FridgeProducts>
    {
        public void Configure(EntityTypeBuilder<FridgeProducts> builder)
        {
            builder.HasData(
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        FridgeId = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"),
                        Quantity = 3
                    },
                    new FridgeProducts
                    {
                        Id = Guid.NewGuid(),
                        ProductId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        FridgeId = new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"),
                        Quantity = 10
                    }
                );
        }
    }
}
