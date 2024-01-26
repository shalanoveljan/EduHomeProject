using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Data.Contexts;
using EduHome.Data.Repositories.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Data.ServiceRegistrations
{
    public static class DataAccessServiceRegisterExtension
    {
        public static void DataAccessServiceRegister(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EduHomeDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));

            });


            services.AddScoped<IAuthorRepository,AuthorRepository>();
            services.AddScoped<IBlogRepository,BlogRepository>();
            services.AddScoped<IEventRepository,EventRepository>();
            services.AddScoped<IPositionOfSpeakerRepository, PositionOfSpeakerRepository>();
            services.AddScoped<ICategoryOfBlogRepository, CategoryOfBlogRepository>();
            services.AddScoped<ICategoryOfEventRepository, CategoryOfEventRepository>();
            services.AddScoped<ITagOfEventRepository, TagOfEventRepository>();
            services.AddScoped<ITagOfBlogRepository, TagOfBlogRepository>();
            services.AddScoped<ISpeakerRepository,SpeakerRepository>();
            services.AddScoped<IDegreeRepository,DegreeRepository>();
            services.AddScoped<IDutyRepository,DutyRepository>();
            services.AddScoped<IFacultyRepository,FacultyRepository>();
            services.AddScoped<ITeacherRepository,TeacherRepository>();
            services.AddScoped<IHobbyRepository,HobbyRepository>();
            services.AddScoped<IBoardRepository,BoardRepository>();
            services.AddScoped<ISliderRepository,SliderRepository>();
            services.AddScoped<ISettingRepository,SettingRepository>();
            services.AddScoped<IContactRepository,ContactRepository>();
            services.AddScoped<ITestimonialRepository,TestimonialRepository>();
            services.AddScoped<ISubcribeRepository, SubcribeRepository>();
            services.AddScoped<IPositionOfTestimonialRepository, PositionOfTestimonialRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();


            services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.Lockout.MaxFailedAccessAttempts = 5;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                opt.SignIn.RequireConfirmedEmail = true;        
            })
                .AddEntityFrameworkStores<EduHomeDbContext>()
                .AddDefaultTokenProviders();



        }

    }
}
