using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class TagOfEvent:BaseEntity
    {
        public string Name { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<TagEvent> TagsEvent { get; set; }


    }
}
