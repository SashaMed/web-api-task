using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Entities
{
    internal class FridgeConfiguration : IEntityTypeConfiguration<Fridge>
    {
        public void Configure(EntityTypeBuilder<Fridge> builder)
        {
            builder.HasData(
                new Fridge
                    {
                        Id = Guid.NewGuid(),
                        Name = "atlant",
                        OwnerName = "sasha",
                        FridgeId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
                    },
                new Fridge
                    {
                        Id = Guid.NewGuid(),
                        Name = "samsung",
                        OwnerName = "anna",
                        FridgeId = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3")
                    }
                );
        }
    }
}
