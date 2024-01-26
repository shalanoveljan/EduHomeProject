using EduHome.Core.DTOs;
using EduHome.Core.Entities;

namespace EduHome.App.ViewModels
{
    public class HomeViewModel
    {
        public List<SliderGetDto> sliders { get; set; }

        public List<SettingGetDto> settings { get; set; }

        public List<TeacherGetDto> teachers { get; set; }

        public List<EventGetDto> events { get; set; }
        public List<BlogGetDto> blogs { get; set; }

        //public List<Subscribe> subscribes { get; set; }
        public List<BoardGetDto> boards { get; set; }
        public List<TestimonialGetDto> testimonials { get; set; }

    }
}
