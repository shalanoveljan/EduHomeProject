using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class SpeakerEvent:BaseEntity
    {
        public int EventId { get; set; }

        public int SpeakerId { get; set; }

        public Event Event { get; set; }

        public Speaker Speaker { get; set; }
    }
}
