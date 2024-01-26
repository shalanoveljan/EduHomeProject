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
    public class CategoryOfBlogConfiguration:IEntityTypeConfiguration<CategoryOfBlog>
    {
        public void Configure(EntityTypeBuilder<CategoryOfBlog> builder)
        {
            builder
     .HasMany(x => x.Blogs)
     .WithOne(x => x.CategoryOfBlog)
     .HasForeignKey(x => x.CategoryIdOfBlog);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
