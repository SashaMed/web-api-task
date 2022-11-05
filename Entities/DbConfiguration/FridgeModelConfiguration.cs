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
    internal class FridgeModelConfiguration : IEntityTypeConfiguration<FridgeModel>
    {
        public void Configure(EntityTypeBuilder<FridgeModel> builder)
        {
            builder.HasData(
                    new FridgeModel
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "super atlant 40Byn/month",
                        Year = 2018
                    },
                    new FridgeModel
                    {
                        Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Name = "samsung ldt123",
                        Year = 2023
                    }
                );
        }
    }
}

