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
    public class TestimonialPositionConfiguration : IEntityTypeConfiguration<TestimonialPosition>
    {
        public void Configure(EntityTypeBuilder<TestimonialPosition> builder)
        {
            builder
                .HasOne(x => x.PositionOfTestimonial)
                .WithMany(x => x.TestimonialPositions)
                .HasForeignKey(x => x.PositionOfTestimonialId);

            builder
             .HasOne(x => x.Testimonial)
             .WithMany(x => x.TestimonialPositions)
             .HasForeignKey(x => x.TestimonialId);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
