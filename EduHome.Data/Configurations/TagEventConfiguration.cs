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
    public class TagEventConfiguration:IEntityTypeConfiguration<TagEvent>
    {
        public void Configure(EntityTypeBuilder<TagEvent> builder)
        {
            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.TagsEvent)
                .HasForeignKey(x => x.EventId);

            builder
             .HasOne(x => x.TagOfEvent)
             .WithMany(x => x.TagsEvent)
             .HasForeignKey(x => x.TagIdOfEvent);

            builder.HasQueryFilter(x => !x.IsDeleted);


        }
    }
}
