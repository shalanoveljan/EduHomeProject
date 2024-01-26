using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Skill:BaseEntity
    {
        public string Key { get; set; }

        public int? Value { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; } = null!;
    }
}
