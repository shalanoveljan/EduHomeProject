using EduHome.Core.DTOs;

namespace EduHome.App.ViewModels
{
    public class AboutViewModel
    {

        public List<SettingGetDto> settings { get; set; }

        public List<TeacherGetDto> teachers { get; set; }

        //public List<Subscribe> subscribes { get; set; }
        public List<BoardGetDto> boards { get; set; }
        public List<TestimonialGetDto> testimonials { get; set; } 
    }
}
