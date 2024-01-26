using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record TeacherGetDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Info { get; set; }

        public string Image { get; set; }

        public int Experience { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public DutyGetDto Duty { get; set; }

        public DegreeGetDto Degree { get; set; }

        public List<string> Icons { get; set; } = null!;
        public List<string> Urls { get; set; } = null!;
        public IEnumerable<FacultyGetDto> Faculties { get; set; } = null!;
        public List<string> SkillKeys { get; set; } = null!;
        public List<int?> SkillValues { get; set; } = null!;
        public List<SocialNetwork> SocialNetworks { get; set; }

        public IEnumerable<HobbyGetDto> Hobbies { get; set; }

    }
}
