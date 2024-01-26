using EduHome.Core.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Teacher : BaseEntity
    {
        public string FullName { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public int Experience { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public int DutyId { get; set; }

        public Duty Duty { get; set; }

        public int DegreeId { get; set; }

        public Degree Degree { get; set; }

        public List<HobbyTeacher> hobbyTeachers { get; set; }

        public List<FacultyTeacher> FacultyTeachers { get; set; }
        public List<SocialNetwork> SocialNetworks { get; set; }
        public List<Skill> Skills { get; set; }



    }
}
