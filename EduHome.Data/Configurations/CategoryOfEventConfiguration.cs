using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Data.Configurations
{
    public class CategoryOfEventConfiguration :IEntityTypeConfiguration<CategoryOfEvent>
    {
        public void Configure(EntityTypeBuilder<CategoryOfEvent> builder)
        {
            builder
     .HasMany(x => x.Events)
     .WithOne(x => x.CategoryOfEvent)
     .HasForeignKey(x => x.CategoryIdOfEvent);


            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
