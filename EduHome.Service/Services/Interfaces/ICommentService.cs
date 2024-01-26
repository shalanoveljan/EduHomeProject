
using EduHome.Core.DTOs;
using EduHome.Core.DTOs.Comment;
using Karma.Service.Responses;

namespace EduHome.Service.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<CommonResponse> CreateAsync(CommentPostDto dto,int id);
    }
}
