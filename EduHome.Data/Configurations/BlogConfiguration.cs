using EduHome.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Data.Configurations
{
    public class BlogConfiguration:IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder
     .HasOne(x => x.Author)
     .WithMany(x => x.Blogs)
     .HasForeignKey(x => x.AuthorId)
     .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.CategoryOfBlog)
                .WithMany(x => x.Blogs)
                .HasForeignKey(x => x.CategoryIdOfBlog);

            builder
                .HasMany(x => x.TagsBlog)
                .WithOne(x => x.Blog)
                .HasForeignKey(x => x.BlogId);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
