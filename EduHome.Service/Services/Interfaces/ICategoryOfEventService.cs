using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ICategoryOfEventService
    {
        public Task<PagginatedResponse<CategoryOfEventGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(CategoryOfEventPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, CategoryOfEventPostDto dto);
        public Task<CategoryOfEventGetDto> GetAsync(int id);
    }
}
