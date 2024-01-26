using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Hobby:BaseEntity
    {
        public string Name { get; set; }

       public List<HobbyTeacher> HobbyTeachers { get; set; } 

    }
}
