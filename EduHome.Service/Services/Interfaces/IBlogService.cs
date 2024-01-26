using EduHome.Core.DTOs;
using EduHome.Core.Entities;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface IBlogService
    {
        public Task<PagginatedResponse<BlogGetDto>> GetAllAsync(int page = 1);

        public Task<CommonResponse> CreateAsync(BlogPostDto dto);
        //Task<List<Blog>> GetBlogsBySearchTextAsync(string searchText);
        public Task RemoveAsync(int id);

        public Task<PagginatedResponse<BlogGetDto>> GetBlogsBySearchTextAsync(string searchText, int page);

        public Task<CommonResponse> UpdateAsync(int id, BlogPostDto dto);
        public Task<BlogGetDto> GetAsync(int id);
    }
}
