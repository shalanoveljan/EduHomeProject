using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Speaker:BaseEntity
    {
		public string FullName { get; set; } = null!;
        public PositionOfSpeaker Position { get; set; } = null!;
        public int PositionId { get; set; }
        public string Image { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public List<SpeakerEvent> SpeakersEvent { get; set; }

    }
}
