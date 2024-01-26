using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Core.DTOs
{
    public class SettingGetDto
    {
        public int Id { get; set; }
        public string Logo { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FacebookUrl { get; set; }
        public string PinterestUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string VimeoUrl { get; set; }
        public string Address { get; set; }
        public string WelcomeTitle { get; set; }
        public string WelcomeDesc { get; set; }
        public string? WelcomeImage { get; set; }
        public string Video { get; set; }


    }
}
