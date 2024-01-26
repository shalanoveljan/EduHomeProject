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
    public class EventConfiguration:IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder
     .HasMany(x => x.SpeakersEvent)
     .WithOne(x => x.Event)
     .HasForeignKey(x => x.EventId);


            builder
                .HasMany(x => x.TagsEvent)
                .WithOne(x => x.Event)
                .HasForeignKey(x => x.EventId);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
