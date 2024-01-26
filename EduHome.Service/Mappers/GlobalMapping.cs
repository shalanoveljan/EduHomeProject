using AutoMapper;
using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Mappers
{

    public class GlobalMapping:Profile
    {

        public GlobalMapping()
        {
            CreateMap<Blog, BlogPostDto>().ReverseMap();
            CreateMap<Blog, BlogGetDto>().ReverseMap();
            CreateMap<Event, EventGetDto>().ReverseMap();
            CreateMap<Event, EventPostDto>().ReverseMap();
            CreateMap<Teacher, TeacherGetDto>().ReverseMap();
            CreateMap<Teacher, TeacherPostDto>().ReverseMap();
            CreateMap<Contact, ContactPostDto>().ReverseMap();
            CreateMap<Contact, ContactGetDto>().ReverseMap();

        }
    }
}
