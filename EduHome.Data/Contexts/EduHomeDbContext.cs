using EduHome.Core.Entities;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EduHome.Data.Contexts
{
    public  class EduHomeDbContext:IdentityDbContext<AppUser>
    {
        public EduHomeDbContext(DbContextOptions<EduHomeDbContext> options):base(options)
        {

        }

        public DbSet<CategoryOfEvent> CategoriesOfEvent { get; set; }
        public DbSet<TagOfEvent> TagsOfEvent { get; set; }
        public DbSet<CategoryOfBlog> CategoriesOfBlog { get; set; }
        public DbSet<TagOfBlog> TagsOfBlog { get; set; }
        public DbSet<TagBlog> TagsBlog { get; set; }
        public DbSet<TagEvent> TagsEvent { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerEvent> SpeakersEvent { get; set; }
        public DbSet<PositionOfSpeaker> PositionsOfSpeaker { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<HobbyTeacher> HobbyTeachers { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<Faculty> Faculties { get; set; }
        public DbSet<Degree> Degrees { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<FacultyTeacher> FacultyTeachers { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Subcribe> Subcribes { get; set; }
        public DbSet<PositionOfTestimonial> PositionOfTestimonials { get; set; }
        public DbSet<TestimonialPosition> TestimonialPositions { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Comment> Comments { get; set; }




        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<Core.Entities.BaseEntities.BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAt = DateTime.UtcNow.AddHours(4);
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAt = DateTime.UtcNow.AddHours(4);
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            //builder.Entity<Comment>().HasOne(x => x.AppUser).WithMany(x=>x.Comments).HasForeignKey(x=>x.Id);
            base.OnModelCreating(builder);
        }

  
    }
}
