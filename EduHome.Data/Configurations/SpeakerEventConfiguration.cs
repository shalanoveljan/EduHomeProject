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
    public class SpeakerEventConfiguration: IEntityTypeConfiguration<SpeakerEvent>
    {
        public void Configure(EntityTypeBuilder<SpeakerEvent> builder)
        {
            builder
         .HasOne(x => x.Event)
         .WithMany(x => x.SpeakersEvent)
         .HasForeignKey(x => x.EventId);

            builder
        .HasOne(x => x.Speaker)
        .WithMany(x => x.SpeakersEvent)
        .HasForeignKey(x => x.SpeakerId);


            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
