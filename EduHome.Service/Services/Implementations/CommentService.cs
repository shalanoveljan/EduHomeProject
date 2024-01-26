using EduHome.Core.DTOs.Comment;
using EduHome.Core.Entities;
using EduHome.Core.Repositories.Interfaces;
using EduHome.Service.Services.Interfaces;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<CommonResponse> CreateAsync(CommentPostDto dto, int id)
        {
            CommonResponse response= new CommonResponse();
            response.StatusCode = 200;
            if (dto.Text == null)
            {
                response.StatusCode = 400;
                return response;
            }

            Comment? comment = new Comment()
            {
                Text = dto.Text,
                CreatedAt = DateTime.Now,
                BlogId = id,
                AspNetUsersId=dto.UserId
            };

            _commentRepository.AddAsync(comment);
            _commentRepository.SaveChangesAsync();

            return response;
        }
    }
}
