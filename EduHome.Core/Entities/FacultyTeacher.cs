using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class FacultyTeacher:BaseEntity
    {
        public int FacultyId { get; set; }

        public Faculty Faculty { get; set; }

        public int TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}
