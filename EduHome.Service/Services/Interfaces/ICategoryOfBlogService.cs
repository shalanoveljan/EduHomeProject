using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ICategoryOfBlogService
    {
        public Task<PagginatedResponse<CategoryOfBlogGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(CategoryOfBlogPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, CategoryOfBlogPostDto dto);
        public Task<CategoryOfBlogGetDto> GetAsync(int id);
    }
}
