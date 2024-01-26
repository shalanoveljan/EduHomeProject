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
    public class SpeakerConfiguration:IEntityTypeConfiguration<Speaker>
    {
        public void Configure(EntityTypeBuilder<Speaker> builder)
        {
            builder
     .HasMany(x => x.SpeakersEvent)
     .WithOne(x => x.Speaker)
     .HasForeignKey(x => x.SpeakerId);



            builder
                .HasOne(x => x.Position)
                .WithMany(x => x.Speakers)
                .HasForeignKey(x => x.PositionId);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
