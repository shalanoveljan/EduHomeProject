using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Data.Contexts;

namespace EduHome.Data.Repositories.Implementations
{
    public class CommentRepository:Repository<Comment>,ICommentRepository
    {
        public CommentRepository(EduHomeDbContext eduHomeDbContext):base(eduHomeDbContext) { }
    }
}
