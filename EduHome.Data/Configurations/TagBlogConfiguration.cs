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
    public class TagBlogConfiguration:IEntityTypeConfiguration<TagBlog>
    {
        public void Configure(EntityTypeBuilder<TagBlog> builder)
        {
            builder
                .HasOne(x => x.Blog)
                .WithMany(x => x.TagsBlog)
                .HasForeignKey(x => x.BlogId);

            builder
             .HasOne(x => x.TagOfBlog)
             .WithMany(x => x.TagsBlog)
             .HasForeignKey(x => x.TagIdOfBlog);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
