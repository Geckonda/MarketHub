using MarketHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.DAL.Configurations
{
    public class BasketEntityProductEntityConfiguration: IEntityTypeConfiguration<BasketEntityProductEntity>
    {

        public void Configure(EntityTypeBuilder<BasketEntityProductEntity> builder)
        {
            //builder.HasKey(b => b.Id);
            //builder
            //   .HasOne(b => b.Basket)
            //   .WithMany(p => p.Baskets);
        }
    }
}
