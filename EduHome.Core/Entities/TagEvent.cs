using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class TagEvent : BaseEntity
    {
        public TagOfEvent TagOfEvent { get; set; }

        public int TagIdOfEvent { get; set; }

        public Event Event { get; set; }

        public int EventId { get; set; }


    }
}
