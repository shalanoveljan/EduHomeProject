using EduHome.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public record TeacherPostDto
    {
        public string FullName { get; set; }

        public string Info { get; set; }

        public IFormFile? ImageFile { get; set; }

        public int Experience { get; set; }
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Skype { get; set; }

        public int DutyId { get; set; }
        public int DegreeId { get; set; }

        public List<string> Icons { get; set; } = null!;
        public List<string> Urls { get; set; } = null!;
        public List<string> SkillKeys { get; set; } = null!;
        public List<int?> SkillValues { get; set; } = null!;

        public List<int> FaculitiesIds { get; set; } = null!;

        public  List<int> HobbiesIds { get; set; } = null!;


    }
}
