using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ITagOfBlogService
    {
        public Task<PagginatedResponse<TagOfBlogGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(TagOfBlogPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, TagOfBlogPostDto dto);
        public Task<TagOfBlogGetDto> GetAsync(int id);
    }
}
