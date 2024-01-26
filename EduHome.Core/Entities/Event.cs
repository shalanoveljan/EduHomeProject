using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Event:BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string JobTime { get; set; } = null!;
        public DateTime EndDate { get; set; }
        public string Image { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public int CategoryIdOfEvent { get; set; }

        public CategoryOfEvent CategoryOfEvent { get; set; }
        public List<TagEvent> TagsEvent { get; set; }

        public List<SpeakerEvent> SpeakersEvent { get; set; }
    }
}
