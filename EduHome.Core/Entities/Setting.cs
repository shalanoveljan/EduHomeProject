using EduHome.Core.Entities.BaseEntities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.Entities
{
    public class Setting:BaseEntity
    {

        public string WelcomeTitle { get; set; }
        public string WelcomeDesc { get; set; }
        public string? WelcomeImage { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string VimeoUrl { get; set; }
        public string Address { get; set; }

        public string Video { get; set; }

    }
}
