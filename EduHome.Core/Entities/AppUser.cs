using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EduHome.Core.Entities
{
    public class AppUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
