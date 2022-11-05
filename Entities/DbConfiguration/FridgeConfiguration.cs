using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Entities.DbConfiguration
{
    internal class FridgeConfiguration : IEntityTypeConfiguration<Fridge>
    {
        public void Configure(EntityTypeBuilder<Fridge> builder)
        {
            builder.HasData(
                new Fridge
                {
                    Id = new Guid("10061240-8b43-47a7-9efe-d2c176cc8bb8"),
                    Name = "atlant",
                    OwnerName = "sasha",
                    FridgeModelId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                },
                new Fridge
                {
                    Id = new Guid("acfff7cb-1804-4ee3-8923-f1e947175f15"),
                    Name = "samsung",
                    OwnerName = "anna",
                    FridgeModelId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                }
                );
        }
    }
}
