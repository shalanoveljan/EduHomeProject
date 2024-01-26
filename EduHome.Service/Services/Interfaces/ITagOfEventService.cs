using EduHome.Core.DTOs;
using Karma.Service.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Service.Services.Interfaces
{
    public interface ITagOfEventService
    {
        public Task<PagginatedResponse<TagOfEventGetDto>> GetAllAsync(int page = 1);

        public Task CreateAsync(TagOfEventPostDto dto);

        public Task RemoveAsync(int id);

        public Task UpdateAsync(int id, TagOfEventPostDto dto);
        public Task<TagOfEventGetDto> GetAsync(int id);
    }
}
