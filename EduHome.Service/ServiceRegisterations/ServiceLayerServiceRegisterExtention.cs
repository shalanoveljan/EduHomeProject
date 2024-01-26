using EduHome.Service.ExternalServices.Implementations;
using EduHome.Service.ExternalServices.Interfaces;
using EduHome.Service.Services.Implementations;
using EduHome.Service.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Karma.Data.ServiceRegisterations
{
	public static class ServiceLayerServiceRegisterExtention
    {
		public static void ServiceLayerServiceRegister(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBlogService, BlogService>();
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<ISpeakerService, SpeakerService>();
            services.AddScoped<IPositionOfSpeakerService,PositionOfSpeakerService>();
            services.AddScoped<ICategoryOfBlogService, CategoryOfBlogService>();
            services.AddScoped<ICategoryOfEventService, CategoryOfEventService>();
            services.AddScoped<ITagOfBlogService, TagOfBlogService>();
            services.AddScoped<ITagOfEventService, TagOfEventService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IFacultyService, FacultyService>();
            services.AddScoped<IDegreeService, DegreeService>();
            services.AddScoped<IDutyService, DutyService>();
            services.AddScoped<ISettingService, SettingService>();
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<IBoardService, BoardService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<ITestimonialService, TestimonialService>();
            services.AddScoped<IPositionOfTestimonialService, PositionOfTestimonialService>();
            services.AddScoped<ISubcribeService, SubcribeService>();
            services.AddScoped<IHobbyService, HobbyService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ICommentService, CommentService>();

        }
    }
}

