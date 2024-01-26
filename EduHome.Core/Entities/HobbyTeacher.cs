using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class HobbyTeacher:BaseEntity
    {
        public int HobbyID { get; set; }

        public Hobby Hobby { get; set; }

        public int TeacherID { get; set; }

        public Teacher Teacher { get; set;}
    }
}
