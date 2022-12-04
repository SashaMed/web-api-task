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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                    new Product
                    {
                        Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                        Name = "watermelon",
                        Description = "yummy",
                        DefaultQuantity = 1,
                        ImagePath = "https://www.uavgusta.net/upload/resize_cache/iblock/d5c/740_740_2/15_faktov_o_roli_vody_v_zhizni_cheloveka.jpg"

                    },
                    new Product
                    {
                        Id = new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"),
                        Name = "milk",
                        Description = "",
                        DefaultQuantity = 3,
                        ImagePath = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b0/Parque_Nacional_de_Bras%C3%ADlia_%2814543997474%29.jpg/274px-Parque_Nacional_de_Bras%C3%ADlia_%2814543997474%29.jpg"
                    }
                );
        }
    }
}
